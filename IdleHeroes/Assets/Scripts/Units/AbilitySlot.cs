using MFCode;

public class AbilitySlot : IUnlockables
{
    protected bool m_isUnlocked;
    public bool IsUnlocked { get => m_isUnlocked; set => m_isUnlocked = value; }

    public AbilitySlot(bool isUnlocked = false)
    {
        m_isUnlocked = isUnlocked;
    }
}