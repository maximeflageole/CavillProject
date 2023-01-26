using UnityEngine;

public class UnitPanelButton : MonoBehaviour
{
    public AutoBattlerUnit AutoBattlerUnit { get; protected set; }
    private TeamListView m_listView;

    public void Initialize(AutoBattlerUnit unit, TeamListView listView)
    {
        m_listView = listView;
        AutoBattlerUnit = unit;
    }

    public void OnClicked()
    {
        m_listView.OnUnitPanelClicked(AutoBattlerUnit);
    }
}
