using System.Collections.Generic;

[System.Serializable]
public class ABUnitDynamicData
{
    public string CharacterName;
    public int Level;
    public ABUnitStats CurrentStats;
    public int UnspentAbilityPoints = 0;
    public List<AbilitySlot> m_abilitySlots;
    public List<EquipmentSlot> m_equipmentSlots;

    public ABUnitDynamicData(string characterName, ABUnitStats stats, int level = 1)
    {
        CharacterName = characterName;
        Level = level;
        CurrentStats = (ABUnitStats)stats.Clone();

        m_abilitySlots = new List<AbilitySlot>();
        for (var i = 0; i < 3; i++)
        {
            m_abilitySlots.Add(new AbilitySlot(i==0));
        }
        m_equipmentSlots = new List<EquipmentSlot>();
        for (var i = 0; i < 3; i++)
        {
            m_equipmentSlots.Add(new EquipmentSlot());
        }
    }

    public void LevelUp()
    {
        if (Level > GameSettings._Instance.LevelUpsSettings.LevelUpRewards.Count) return;

        var levelRewards = GameSettings._Instance.LevelUpsSettings.LevelUpRewards[Level-1];
        foreach (var reward in levelRewards.LevelRewardsList)
        {
            switch (reward)
            {
                case ELevelUpsRewardType.AbilityPoint:
                    UnspentAbilityPoints++;
                    break;
                case ELevelUpsRewardType.AbilitySlot:
                    UnlockAbilitySlot();
                    break;
                case ELevelUpsRewardType.EquipmentSlot:
                    UnlockEquipmentSlot();
                    break;
                case ELevelUpsRewardType.StatUpgrade:
                    //TODO
                    break;
                case ELevelUpsRewardType.Count:
                    break;
            }
        }
        Level++;
    }

    protected void UnlockEquipmentSlot()
    {
        var i = 0;
        foreach (var equipmentSlot in m_equipmentSlots)
        {
            if (!equipmentSlot.IsUnlocked)
            {
                equipmentSlot.IsUnlocked = true;
                return;
            }
            i++;
        }
    }

    protected void UnlockAbilitySlot()
    {
        var i = 0;
        foreach (var abilitySlot in m_abilitySlots)
        {
            if (!abilitySlot.IsUnlocked)
            {
                abilitySlot.IsUnlocked = true;
                return;
            }
            i++;
        }
    }

    public List<StatTuple> GetCurrentStatsTuples()
    {
        var returnValue = new List<StatTuple>();
        returnValue.Add(CurrentStats.Constitution);
        returnValue.Add(CurrentStats.Strength);
        returnValue.Add(CurrentStats.AttackSpeed);
        returnValue.Add(CurrentStats.Precision);
        returnValue.Add(CurrentStats.AbilityPower);
        returnValue.Add(CurrentStats.ManaManagement);

        return returnValue;
    }
}

    [System.Serializable]
public class ABUnitStats
{
    public object Clone()
    {
        return MemberwiseClone();
    }
    public float MaxHealth;
    public float MaxMana;
    public float BattleBeginMana;
    public float Armor;

    public StatTuple Constitution;
    public StatTuple Strength;
    public StatTuple AttackSpeed;
    public StatTuple Precision;
    public StatTuple AbilityPower;
    public StatTuple ManaManagement;
}