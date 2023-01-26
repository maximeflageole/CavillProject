using UnityEngine;

public class AutoBattlerUnit : MonoBehaviour
{
    [field: SerializeField]
    public AutoBattlerUnitData UnitData { get; protected set; }
    protected UnitStats UnitStats;

    private void Start()
    {
        UnitStats = new UnitStats(UnitData.UnitStats, UnitData.UnitPerLevelGrowth, 1);
    }

    public void SetData(AutoBattlerUnitData data)
    {
        UnitData = data;
    }
}
