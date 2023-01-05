using System.Collections.Generic;
using UnityEngine;

public class AutoBattlerTeam : MonoBehaviour
{
    [field:SerializeField]
    public bool IsPlayerTeam { get; protected set; }
    [field:SerializeField]
    public List<AutoBattlerUnit> AutoBattlerUnits { get; protected set; }

    private void Start()
    {
        AutoBattlerUnits.Clear();
        var units = GetComponentsInChildren<AutoBattlerUnit>();

        foreach (var unit in units)
        {
            AutoBattlerUnits.Add(unit);
            unit.IsInPlayerTeam = IsPlayerTeam;
        }
    }

    private bool HasUnitAtIndex(int index)
    {
        return (AutoBattlerUnits.Count > index);
    }

    public AutoBattlerUnit GetUnitAtIndex(int index)
    {
        if (HasUnitAtIndex(index))
        {
            return AutoBattlerUnits[index];
        }
        return null;
    }

    public void OnUnitDeath()
    {

    }
}
