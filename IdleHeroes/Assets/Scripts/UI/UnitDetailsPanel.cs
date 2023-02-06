using UnityEngine;

public class UnitDetailsPanel : MonoBehaviour
{
    public ABUnit ABUnit { get; protected set; }

    public void Initialize(ABUnit unit)
    {
        ABUnit = unit;
    }

    public void OnAbilityTreeBtnClicked()
    {

    }
}
