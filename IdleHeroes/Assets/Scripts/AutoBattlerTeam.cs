using System.Collections.Generic;
using UnityEngine;

public class AutoBattlerTeam : MonoBehaviour
{
    public GameObject UnitPrefab;
    [field:SerializeField]
    public bool IsPlayerTeam { get; protected set; }
    [field:SerializeField]
    public List<AutoBattlerUnit> AutoBattlerUnits { get; protected set; }
    [field: SerializeField]
    public List<AutoBattlerBattleInstance> AutoBattlerBattleInstances { get; protected set; }

    private void Start()
    {
        AutoBattlerUnits.Clear();
        var units = GetComponentsInChildren<AutoBattlerBattleInstance>();

        foreach (var unit in units)
        {
            AutoBattlerBattleInstances.Add(unit);
            unit.RegisterTeam(this, IsPlayerTeam);
        }
    }

    public void LoadTeam(TeamData teamData)
    {
        foreach (var unitData in teamData.TeamUnits)
        {
            var unitInstance = Instantiate(UnitPrefab, transform).GetComponent<AutoBattlerUnit>();
            AutoBattlerUnits.Add(unitInstance);
            unitInstance.SetData(unitData);
        }
    }

    private bool HasUnitAtIndex(int index)
    {
        return (AutoBattlerUnits.Count > index);
    }

    public AutoBattlerBattleInstance GetUnitAtIndex(int index)
    {
        if (HasUnitAtIndex(index))
        {
            return AutoBattlerBattleInstances[index];
        }
        return null;
    }

    public void OnUnitDeath(AutoBattlerBattleInstance deadUnit)
    {
        AutoBattlerBattleInstances.Remove(deadUnit);
        BattleManager.Instance.VerifyBattleEnding();
    }
}
