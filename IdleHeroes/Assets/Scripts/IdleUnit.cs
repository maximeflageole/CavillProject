using UnityEngine;

public class IdleUnit
{
    [SerializeField]
    protected int m_currentHealth;
    [field: SerializeField]
    public IdleUnitData UnitData { get; protected set; }
    [field: SerializeField]
    public float CurrentAttackTimer { get; protected set; } = 0.0f;
    [field: SerializeField]
    public LocationPanel LocationPanel { get; protected set; }

    public IdleUnit(IdleUnitData unitData)
    {
        Debug.Log("Unit " + unitData.UnitName +  " created");
        UnitData = unitData;
        m_currentHealth = UnitData.BaseHealth;
    }

    public void UpdateAttackTimer()
    {
        CurrentAttackTimer += Time.deltaTime;
        if (CurrentAttackTimer > UnitData.AttackSpeed)
        {
            CurrentAttackTimer %= UnitData.AttackSpeed;
        }
    }
}
