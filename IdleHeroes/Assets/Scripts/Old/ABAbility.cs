using System.Collections.Generic;

public class ABAbility
{
    public ABBattleInstance Origin;
    public AbilityData AbilityData;

    public ABAbility(ABBattleInstance origin, AbilityData abilityData)
    {
        Origin = origin;
        AbilityData = abilityData;
    }
}

[System.Serializable]
public enum EEffectType
{
    Damage,
    Heal,
    Count
}

[System.Serializable]
public enum ETargetType
{
    Melee,
    Self,
    Ally,
    Count
}

[System.Serializable]
public struct SActionEffect
{
    public EEffectType Effect;
    public List<ETargetType> TargetTypes;
}

[System.Serializable]
public struct SActionEffects
{
    public List<SActionEffect> actionEffects;
}