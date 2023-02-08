using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level ups settings", menuName = "Settings/Level Ups", order = 0)]
public class LevelUpsSettings : ScriptableObject
{
    public List<LevelRewards> LevelUpRewards = new List<LevelRewards>();
}

public enum ELevelUpsRewardType
{
    AbilityPoint,
    AbilitySlot,
    EquipmentSlot,
    StatUpgrade,
    Count
}

[System.Serializable]
public class LevelRewards
{
    public List<ELevelUpsRewardType> LevelRewardsList = new List<ELevelUpsRewardType>();
}