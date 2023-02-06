using System.Collections.Generic;
using UnityEngine;

public class HubMainPanel : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_teamScrollView;
    [SerializeField]
    protected GameObject m_worldPanel;
    [SerializeField]
    protected GameObject m_missionPanel;
    [SerializeField]
    protected GameObject m_unitDetailsPanel;
    [SerializeField]
    protected GameObject m_abilityTreeUI;

    [field: SerializeField]
    public TeamListView TeamListView { get; protected set; }
    [field: SerializeField]
    public MissionsPanel MissionsPanel { get; protected set; }
    [SerializeField]
    private List<LocationData> m_locationsData = new List<LocationData>();

    private void Start()
    {
        TeamListView.Instantiate(this);
        MissionsPanel.Instantiate(this, m_locationsData);
    }

    private void CloseEveryPanel()
    {
        m_worldPanel.SetActive(false);
        m_unitDetailsPanel.SetActive(false);
        m_missionPanel.SetActive(false);
        m_abilityTreeUI.SetActive(false);
    }

    public void OnAbilityTreeBtnClicked()
    {
        CloseEveryPanel();
        m_abilityTreeUI.SetActive(true);
    }

    public void OnExitBtnClicked()
    {
        CloseEveryPanel();
        m_unitDetailsPanel.SetActive(true);
    }

    public void OnUnitClicked(ABUnit unit)
    {
        CloseEveryPanel();
        m_unitDetailsPanel.gameObject.SetActive(true);
    }

    public void OnWorldBtnClicked()
    {
        CloseEveryPanel();
        m_worldPanel.gameObject.SetActive(true);
    }

    public void OnMissionBtnClicked()
    {
        CloseEveryPanel();
        m_missionPanel.gameObject.SetActive(true);
    }
}
