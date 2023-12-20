using System.Collections.Generic;
using UnityEngine;

public class MissionsPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject m_missionBtnPrefab;
    private HubMainPanel m_mainPanel;

    public void Instantiate(HubMainPanel mainPanel, List<LocationData> locationsData)
    {
        m_mainPanel = mainPanel;
        foreach (var locationData in locationsData)
        {
            var missionBtn = Instantiate(m_missionBtnPrefab, transform).GetComponent<MissionButton>();
            missionBtn.Instantiate(locationData);
        }
    }
}
