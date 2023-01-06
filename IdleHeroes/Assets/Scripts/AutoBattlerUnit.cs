using MoreMountains.Feedbacks;
using MRF.Containers;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class AutoBattlerUnit: MonoBehaviour
{
    protected AutoBattlerTeam m_team;
    public bool IsInPlayerTeam { get; protected set; }
    protected CancellationTokenSource m_cancellationToken;

    [SerializeField]
    protected Container m_healthContainer;
    [SerializeField]
    protected Image m_healthBar;
    [field: SerializeField]
    public AutoBattlerUnitData UnitData { get; protected set; }
    [field: SerializeField]
    public List<float> CurrentAbilitiesTimers { get; protected set; } = new List<float>();
    [SerializeField]
    protected List<BattleAbilityUI> m_abilitiesUI = new List<BattleAbilityUI>();

    //FEEL Effects
    [SerializeField]
    protected MMF_Player m_attackEffect;

    //Actions
    public System.Action<AutoBattlerUnit> OnUnitDeath;

    private void Start()
    {
        m_healthContainer = new Container(UnitData.BaseHealth);
        m_healthContainer.OnEmptyCallback += OnHealthEmptied;

        for (int i = 0; i < UnitData.UnitAbilities.Count; i++)
        {
            var ability = UnitData.UnitAbilities[i];
            m_abilitiesUI[i].EquipAbility(ability.Sprite);
            CurrentAbilitiesTimers.Add(0);
        }
    }

    void Update()
    {
        UpdateHealth();
        if (BattleManager.Instance.AbilityInProgress) return;
        UpdateAbilitiesTimer();
        UpdateAbilitiesUI();
    }

    public void RegisterTeam(AutoBattlerTeam team, bool isPlayerTeam)
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
        foreach (var ability in UnitData.UnitAbilities)
        {
            m_abilitiesUI[i].UpdateImageFillAmount(CurrentAbilitiesTimers[i] / UnitData.UnitAbilities[i].Cooldown);
            i++;
        }
    }

    public void UpdateAbilitiesTimer()
    {
        var i = 0;
        foreach (var ability in UnitData.UnitAbilities)
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

    private void ExecuteAbility(int index)
    {
        BattleManager.Instance.QueueAbility(this, UnitData.UnitAbilities[index].AbilityData);
    }

    public async void BeginAbility()
    {
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

    public void ExecuteEffect(AutoBattlerUnit target, EEffectType effectType)
    {
        switch (effectType)
        {
            case EEffectType.Damage:
                DamageUnit(target);
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

    protected void DamageUnit(AutoBattlerUnit target)
    {
        target.ReceiveDamage(UnitData.BaseDamage);
    }

    protected void HealUnit(AutoBattlerUnit target)
    {
        target.ReceiveHealing(UnitData.BaseDamage);
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
}