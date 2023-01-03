using System.Collections.Generic;

public class AutoBattlerAction
{
    public AutoBattlerUnit Origin;
    public AbilityData AbilityData;

    public AutoBattlerAction(AutoBattlerUnit origin, AbilityData abilityData)
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