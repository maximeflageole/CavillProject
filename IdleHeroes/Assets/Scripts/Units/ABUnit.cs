using UnityEngine;

public class ABUnit : MonoBehaviour
{
    [field: SerializeField]
    public ABUnitStaticData UnitStaticData { get; protected set; }
    [field: SerializeField]
    public ABUnitDynamicData UnitDynamicData { get; protected set; }

    private void Start()
    {
        UnitDynamicData = new ABUnitDynamicData(UnitStaticData.UnitName, UnitStaticData.UnitBaseStats, 1);
    }

    public void SetData(ABUnitStaticData data)
    {
        UnitStaticData = data;
    }

    public void LevelUp()
    {
        UnitDynamicData.LevelUp();
    }

    public float GetAttackTime()
    {
        return UnitDynamicData.CurrentStats.AttackSpeed.Value;
    }
}

public enum EPrimaryStatistic
{
    Constitution,
    Strength,
    AttackSpeed,
    Precision,
    AbilityPower,
    ManaManagement,
    Count
}

public enum ESecondaryStatistic
{
    Armor,
    Resistance,
    Health,
    Mana,
    Count
}