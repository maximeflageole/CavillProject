using UnityEngine;

public class UnitDetailsPanel : MonoBehaviour
{
    public AutoBattlerUnit AutoBattlerUnit { get; protected set; }

    public void Initialize(AutoBattlerUnit unit)
    {
        AutoBattlerUnit = unit;
    }

    public void OnAbilityTreeBtnClicked()
    {

    }
}
