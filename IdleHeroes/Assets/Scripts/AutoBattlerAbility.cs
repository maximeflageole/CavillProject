using System.Collections.Generic;

public class AutoBattlerAbility
{
    public AutoBattlerBattleInstance Origin;
    public AbilityData AbilityData;

    public AutoBattlerAbility(AutoBattlerBattleInstance origin, AbilityData abilityData)
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