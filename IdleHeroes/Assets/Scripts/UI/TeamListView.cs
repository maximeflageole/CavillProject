using UnityEngine;

public class TeamListView : MonoBehaviour
{
    public GameObject m_characterButtonPrefab;
    private HubMainPanel m_mainPanel;

    public void Instantiate(HubMainPanel mainPanel)
    {
        m_mainPanel = mainPanel;
    }

    public void Instantiate(ABTeam ABTeam)
    {
        foreach (var unit in ABTeam.ABUnits)
        {
            var unitBtnInstance = Instantiate(m_characterButtonPrefab, transform).GetComponent<UnitPanelButton>();
            unitBtnInstance.Initialize(unit, this);
        }
    }

    public void OnUnitPanelClicked(ABUnit unit)
    {
        m_mainPanel.OnUnitClicked(unit);
    }
}
