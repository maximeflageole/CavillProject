using MFCode;

public class EquipmentSlot : IUnlockables
{
    protected bool m_isUnlocked = false;
    public bool IsUnlocked { get => m_isUnlocked; set => m_isUnlocked = value; }

    public EquipmentSlot(bool isUnlocked = false)
    {
        m_isUnlocked = isUnlocked;
    }
}