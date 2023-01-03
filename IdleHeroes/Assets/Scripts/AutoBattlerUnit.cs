using MoreMountains.Feedbacks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoBattlerUnit: MonoBehaviour
{
    public bool IsInPlayerTeam { get; set; }
    [SerializeField]
    protected float m_currentHealth;
    [SerializeField]
    protected Image m_healthBar;
    [field: SerializeField]
    public AutoBattlerUnitData UnitData { get; protected set; }
    [field: SerializeField]
    public List<float> CurrentAbilitiesTimers { get; protected set; } = new List<float>();
    [SerializeField]
    protected MMF_Player m_attackEffect;
    [SerializeField]
    protected List<BattleAbilityUI> m_abilitiesUI = new List<BattleAbilityUI>();

    private void Start()
    {
        m_currentHealth = UnitData.BaseHealth;
        for (int i = 0; i < UnitData.UnitAbilities.Count; i++)
        {
            var ability = UnitData.UnitAbilities[i];
            m_abilitiesUI[i].EquipAbility(ability.Sprite);
            CurrentAbilitiesTimers.Add(0);
        }
    }

    void Update()
    {
        if (BattleManager.Instance.ActionInProgress) return;
        UpdateAbilitiesTimer();
        UpdateVisuals();
    }

    protected void UpdateVisuals()
    {
        var i = 0;
        foreach (var ability in UnitData.UnitAbilities)
        {
            m_abilitiesUI[i].UpdateImageFillAmount(CurrentAbilitiesTimers[i] / UnitData.UnitAbilities[i].Cooldown);
            i++;
        }
        m_healthBar.fillAmount = m_currentHealth / UnitData.BaseHealth;
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
        m_currentHealth -= damage;
    }

    public void ReceiveHealing(float healAmount)
    {
        m_currentHealth += healAmount;
    }
}