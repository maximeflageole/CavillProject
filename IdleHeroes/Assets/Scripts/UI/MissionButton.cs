using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_missionBiomeName;
    [SerializeField]
    private Image m_biomeImage;

    public void Instantiate(LocationData locationData)
    {
        m_missionBiomeName.text = locationData.LocationName;
        m_biomeImage.sprite = locationData.LocationSprite;
    }

    public void OnClick()
    {
        GameManager._Instance.HubMainPanel.OnMissionBtnClicked();
    }
}
