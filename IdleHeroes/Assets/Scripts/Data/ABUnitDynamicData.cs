using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ABUnitDynamicData
{
    public string CharacterName;
    public int Level;
    public ABUnitStats CurrentStats;

    public ABUnitDynamicData(string characterName, ABUnitStats stats, int level = 1)
    {
        CharacterName = characterName;
        Level = level;
        CurrentStats = stats;
    }
}

[System.Serializable]
public class ABUnitStats
{
    public float MaxHealth;
    public float MaxMana;
    public float BattleBeginMana;

    public float Constitution;
    public float Strength;
    public float AttackSpeed;
    public float Precision;
    public float AbilityPower;
    public float ManaManagement;
}
