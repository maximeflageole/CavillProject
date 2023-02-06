using UnityEngine;

public class UnitPanelButton : MonoBehaviour
{
    public ABUnit ABUnit { get; protected set; }
    private TeamListView m_listView;

    public void Initialize(ABUnit unit, TeamListView listView)
    {
        m_listView = listView;
        ABUnit = unit;
    }

    public void OnClicked()
    {
        m_listView.OnUnitPanelClicked(ABUnit);
    }
}
