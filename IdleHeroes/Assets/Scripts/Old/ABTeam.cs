using System.Collections.Generic;
using UnityEngine;

public class ABTeam : MonoBehaviour
{
    public GameObject UnitPrefab;
    [field:SerializeField]
    public bool IsPlayerTeam { get; protected set; }
    [field:SerializeField]
    public List<ABUnit> ABUnits { get; protected set; }
    [field: SerializeField]
    public List<ABBattleInstance> ABBattleInstances { get; protected set; }

    private void Start()
    {
        ABUnits.Clear();
        ABBattleInstances.Clear();
        var units = GetComponentsInChildren<ABBattleInstance>();

        foreach (var unit in units)
        {
            ABUnits.Add(unit.GetComponent<ABUnit>());
            ABBattleInstances.Add(unit);
            unit.RegisterTeam(this, IsPlayerTeam);
        }
    }

    public void LoadTeam(TeamData teamData)
    {
        foreach (var unitData in teamData.TeamUnits)
        {
            var unitInstance = Instantiate(UnitPrefab, transform).GetComponent<ABUnit>();
            ABUnits.Add(unitInstance);
            unitInstance.SetData(unitData);
        }
    }

    private bool HasUnitAtIndex(int index)
    {
        return (ABUnits.Count > index);
    }

    public ABBattleInstance GetUnitAtIndex(int index)
    {
        if (HasUnitAtIndex(index))
        {
            return ABBattleInstances[index];
        }
        return null;
    }

    public void OnUnitDeath(ABBattleInstance deadUnit)
    {
        ABBattleInstances.Remove(deadUnit);
        BattleManager.Instance.VerifyBattleEnding();
    }
}
