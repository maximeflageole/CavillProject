using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Team", menuName = "ScriptableObjects/TeamData", order = 3)]
public class TeamData : ScriptableObject
{
    public List<AutoBattlerUnitData> TeamUnits = new List<AutoBattlerUnitData>();
}