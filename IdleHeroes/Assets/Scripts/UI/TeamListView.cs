using UnityEngine;

public class TeamListView : MonoBehaviour
{
    public GameObject m_characterButtonPrefab;
    private HubMainPanel m_mainPanel;

    public void Instantiate(HubMainPanel mainPanel)
    {
        m_mainPanel = mainPanel;
    }

    public void Instantiate(AutoBattlerTeam autoBattlerTeam)
    {
        foreach (var unit in autoBattlerTeam.AutoBattlerUnits)
        {
            var unitBtnInstance = Instantiate(m_characterButtonPrefab, transform).GetComponent<UnitPanelButton>();
            unitBtnInstance.Initialize(unit, this);
        }
    }

    public void OnUnitPanelClicked(AutoBattlerUnit unit)
    {
        m_mainPanel.OnUnitClicked(unit);
    }
}
