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

    private void Start()
    {
        TeamListView.Instantiate(this);
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
