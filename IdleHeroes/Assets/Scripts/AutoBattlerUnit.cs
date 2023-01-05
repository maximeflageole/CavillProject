using MoreMountains.Feedbacks;
using MRF.Containers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoBattlerUnit: MonoBehaviour
{
    public bool IsInPlayerTeam { get; set; }
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
        if (BattleManager.Instance.ActionInProgress) return;
        UpdateAbilitiesTimer();
        UpdateVisuals();
    }

    protected void UpdateHealth()
    {
        m_healthBar.fillAmount = m_healthContainer.CurrentValue / m_healthContainer.GetMaxValue();
    }

    protected void UpdateVisuals()
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
                ExecuteAction(i);
            }
            i++;
        }
    }

    private void ExecuteAction(int index)
    {
        BattleManager.Instance.QueueAction(this, UnitData.UnitAbilities[index].AbilityData);
    }

    public async void BeginAction()
    {
        await m_attackEffect.PlayFeedbacksTask(transform.position);
        OnAbilityVisualEffectComplete();
    }

    public void OnAbilityVisualEffectComplete()
    {
        BattleManager.Instance.StopAction();
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
        Destroy(gameObject);
    }
}