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
}