using MoreMountains.Feedbacks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoBattlerUnit: MonoBehaviour
{
    [SerializeField]
    protected float m_currentHealth;
    [SerializeField]
    protected Image m_healthBar;
    [field: SerializeField]
    public AutoBattlerUnitData UnitData { get; protected set; }
    [field: SerializeField]
    public float CurrentAttackTimer { get; protected set; } = 0.0f;
    [SerializeField]
    protected MMF_Player m_attackEffect;
    [SerializeField]
    protected List<Image> m_abilitiesImages = new List<Image>();

    private void Start()
    {
        m_currentHealth = UnitData.BaseHealth;
    }

    void Update()
    {
        UpdateAutoAttackTimer();
        UpdateVisuals();
    }

    protected void UpdateVisuals()
    {
        m_abilitiesImages[0].fillAmount = CurrentAttackTimer / UnitData.AttackSpeed;
        m_healthBar.fillAmount = m_currentHealth / UnitData.BaseHealth;
    }

    public void UpdateAutoAttackTimer()
    {
        CurrentAttackTimer += Time.deltaTime;
        m_abilitiesImages[0].fillAmount = CurrentAttackTimer / UnitData.AttackSpeed;
        if (CurrentAttackTimer > UnitData.AttackSpeed)
        {
            CurrentAttackTimer %= UnitData.AttackSpeed;
            AutoAttack();
        }
    }

    private void AutoAttack()
    {
        BattleManager.Instance.QueueAction(this);
    }

    public void BeginAction()
    {
        m_attackEffect?.PlayFeedbacks();
    }

    public void OnAbilityVisualEffectComplete()
    {
        BattleManager.Instance.StopAction();
    }
}
