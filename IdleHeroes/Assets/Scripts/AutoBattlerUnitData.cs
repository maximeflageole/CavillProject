using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObjects/Unit", order = 1)]
public class AutoBattlerUnitData : ScriptableObject
{
    [field: SerializeField]
    public string UnitName;
    [field:SerializeField]
    public Sprite UnitSprite { get; protected set; }

    [field: SerializeField]
    public float BaseHealth { get; protected set; } = 100; //Health will represent how long a hero can stay on a mission!
    [field: SerializeField]
    public float BaseDamage { get; protected set; } = 10;
    [field: SerializeField]
    public float AttackSpeed { get; protected set; } = 2.0f;

    public List<UnitAbility> UnitAbilities = new List<UnitAbility>();
}

[System.Serializable]
public struct UnitAbility
{
    public AbilityData AbilityData;
    public float Cooldown;
    public Sprite Sprite;
}