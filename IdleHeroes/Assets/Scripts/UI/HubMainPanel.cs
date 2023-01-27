using System.Collections.Generic;
using UnityEngine;

public class HubMainPanel : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_teamScrollView;
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

    public void OnAbilityTreeBtnClicked()
    {
        m_unitDetailsPanel.SetActive(false);
        m_abilityTreeUI.SetActive(true);
    }

    public void OnExitBtnClicked()
    {
        m_abilityTreeUI.SetActive(false);
        m_unitDetailsPanel.SetActive(true);
    }

    public void OnUnitClicked(AutoBattlerUnit unit)
    {
        m_missionPanel.gameObject.SetActive(false);
        m_unitDetailsPanel.gameObject.SetActive(true);
    }

    public void OnMissionsBtnClicked()
    {
        m_unitDetailsPanel.gameObject.SetActive(false);
        m_missionPanel.gameObject.SetActive(true);
    }
}