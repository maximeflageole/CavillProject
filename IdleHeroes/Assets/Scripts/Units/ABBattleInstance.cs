using MoreMountains.Feedbacks;
using MRF.Containers;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ABBattleInstance: MonoBehaviour
{
    protected ABTeam m_team;
    public bool IsInPlayerTeam { get; protected set; }
    protected CancellationTokenSource m_cancellationToken;

    [SerializeField]
    protected Container m_healthContainer;
    [SerializeField]
    protected Image m_healthBar;
    public ABUnit m_unitInstance;

    public float AutoAttackTimer { get; protected set; }
    [field: SerializeField]
    public List<float> CurrentAbilitiesTimers { get; protected set; } = new List<float>();
    [SerializeField]
    protected List<BattleAbilityUI> m_abilitiesUI = new List<BattleAbilityUI>();

    //FEEL Effects
    [SerializeField]
    protected MMF_Player m_attackEffect;

    //Actions
    public System.Action<ABBattleInstance> OnUnitDeath;

    private void Start()
    {
        Debug.Log(gameObject.name);
        m_healthContainer = new Container(m_unitInstance.UnitDynamicData.CurrentStats.MaxHealth);
        m_healthContainer.OnEmptyCallback += OnHealthEmptied;

        AutoAttackTimer = 0;

        for (int i = 0; i < m_unitInstance.UnitStaticData.UnitAbilities.Count; i++)
        {
            var ability = m_unitInstance.UnitStaticData.UnitAbilities[i];
            //m_abilitiesUI[i].EquipAbility(ability.Sprite);
            CurrentAbilitiesTimers.Add(0);
        }
    }

    void Update()
    {
        UpdateHealth();
        if (BattleManager.Instance.AbilityInProgress) return;
        UpdateAutoAttackTimer();
    }

    public void RegisterTeam(ABTeam team, bool isPlayerTeam)
    {
        IsInPlayerTeam = isPlayerTeam;
        m_team = team;
        OnUnitDeath += m_team.OnUnitDeath;
    }

    protected void UpdateHealth()
    {
        m_healthBar.fillAmount = m_healthContainer.CurrentValue / m_healthContainer.GetMaxValue();
    }

    protected void UpdateAbilitiesUI()
    {
        var i = 0;
        foreach (var ability in m_unitInstance.UnitStaticData.UnitAbilities)
        {
            m_abilitiesUI[i].UpdateImageFillAmount(CurrentAbilitiesTimers[i] / m_unitInstance.UnitStaticData.UnitAbilities[i].Cooldown);
            i++;
        }
    }

    public void UpdateAbilitiesTimer()
    {
        var i = 0;
        foreach (var ability in m_unitInstance.UnitStaticData.UnitAbilities)
        {
            CurrentAbilitiesTimers[i] += Time.deltaTime;
            if (CurrentAbilitiesTimers[i] > ability.Cooldown)
            {
                CurrentAbilitiesTimers[i] %= ability.Cooldown;
                ExecuteAbility(i);
            }
            i++;
        }
    }

    public void UpdateAutoAttackTimer()
    {
        AutoAttackTimer += Time.deltaTime;
        if (AutoAttackTimer > m_unitInstance.GetAttackTime())
        {
            AutoAttackTimer %= m_unitInstance.GetAttackTime();
            ExecuteAutoAttack();
        }
    }

    private void ExecuteAutoAttack()
    {
        BattleManager.Instance.QueueAbility(this, m_unitInstance.UnitStaticData.AutoAttack.AbilityData);
    }

    private void ExecuteAbility(int index)
    {
        BattleManager.Instance.QueueAbility(this, m_unitInstance.UnitStaticData.UnitAbilities[index].AbilityData);
    }

    public async void BeginAbility()
    {
        Debug.Log("Ability begun ");
        m_cancellationToken = new CancellationTokenSource();
        try
        {
            await m_attackEffect.PlayFeedbacksTask(transform.position);
        }
        finally
        {
            m_cancellationToken.Dispose();
            m_cancellationToken = null;
        }
        OnAbilityVisualEffectComplete();
    }

    public void OnAbilityVisualEffectComplete()
    {
        BattleManager.Instance.OnAbilityAnimationOver();
    }

    public void ExecuteEffect(ABBattleInstance target, EEffectType effectType)
    {
        //TODO MF: This should be executed in BattleManager, never here
        switch (effectType)
        {
            case EEffectType.Damage:
                DamageUnit(target, BattleManager.Instance.CalculateAutoAttackDamage(this, target));
                break;
            case EEffectType.Heal:
                HealUnit(target);
                break;
            case EEffectType.Count:
                break;
            default:
                break;
        }
    }

    protected void DamageUnit(ABBattleInstance target, float damageAmount)
    {
        target.ReceiveDamage(damageAmount);
    }

    protected void HealUnit(ABBattleInstance target)
    {
        target.ReceiveHealing(m_unitInstance.UnitStaticData.BaseDamage);
    }

    public void ReceiveDamage(float damage)
    {
        m_healthContainer.RemoveValue(damage);
    }

    public void ReceiveHealing(float healAmount)
    {
        m_healthContainer.AddValue(healAmount);
    }

    public bool IsAlive()
    {
        return !m_healthContainer.IsEmpty();
    }

    protected void OnHealthEmptied()
    {
        OnUnitDeath?.Invoke(this);
        m_cancellationToken?.Cancel();
        Destroy(gameObject);
    }

    public float GetAABaseDamage()
    {
        //TODO MF
        return m_unitInstance.UnitDynamicData.CurrentStats.Strength.Value * GetWeaponModifier();
    }

    public float GetArmor()
    {
        return m_unitInstance.UnitDynamicData.CurrentStats.Armor;
    }

    protected float GetWeaponModifier()
    {
        //TODO MF
        return 1f;
    }
}