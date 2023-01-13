using UnityEngine;

public class MainPanel : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_teamScollView;
    [SerializeField]
    protected GameObject m_missionPanel;
    [SerializeField]
    protected GameObject m_unitDetailsPanel;
    [SerializeField]
    protected GameObject m_abilityTreeUI;

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
}
