using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObjects/Unit", order = 1)]
public class AutoBattlerUnitData : ScriptableObject
{
    [field: SerializeField]
    public string UnitName;
    [field:SerializeField]
    public Sprite UnitSprite { get; protected set; }

    public UnitStats UnitStats;
    public UnitPerLevelGrowth UnitPerLevelGrowth;

    [field: SerializeField]
    public float BaseDamage { get; protected set; } = 10;

    public List<UnitAbility> UnitAbilities = new List<UnitAbility>();
}

[System.Serializable]
public struct UnitAbility
{
    public AbilityData AbilityData;
    public float Cooldown;
    public Sprite Sprite;
}

[System.Serializable]
public class UnitStats
{
    public UnitStats(float castSpeed, float baseHealth, float baseDamage, int currentLevel = 1)
    {
        CastSpeed = castSpeed;
        BaseHealth = baseHealth;
        BaseDamage = baseDamage;
        CurrentLevel = currentLevel;
    }

    public UnitStats(UnitStats stats, UnitPerLevelGrowth perLevelGrowthData, int currentLevel = 1)
    {
        CastSpeed = stats.CastSpeed + (currentLevel-1) * perLevelGrowthData.CastSpeedGrowth;
        BaseHealth = stats.BaseHealth + (currentLevel - 1) * perLevelGrowthData.MaxHealthGrowth;
        BaseDamage = stats.BaseDamage + (currentLevel - 1) * perLevelGrowthData.DamageGrowth;
        CurrentLevel = currentLevel;
    }

    [field: SerializeField]
    public float CastSpeed { get; protected set; } = 10.0f;
    [field: SerializeField]
    public float BaseHealth { get; protected set; } = 100.0f; //Health will represent how long a hero can stay on a mission!
    [field: SerializeField]
    public float BaseDamage { get; protected set; } = 10.0f;
    public int CurrentLevel = 1;
}

[System.Serializable]
public struct UnitPerLevelGrowth
{
    public float CastSpeedGrowth;
    public float MaxHealthGrowth;
    public float DamageGrowth;
}