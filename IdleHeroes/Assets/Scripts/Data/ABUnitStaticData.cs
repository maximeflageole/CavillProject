using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObjects/Unit", order = 1)]
public class ABUnitStaticData : ScriptableObject
{
    [field: SerializeField]
    public string UnitName;
    [field:SerializeField]
    public Sprite UnitSprite { get; protected set; }

    public ABUnitStats UnitBaseStats;
    public ABUnitStats UnitPerLevelGrowth;

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