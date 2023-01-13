using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocationPanel : MonoBehaviour
{
    protected bool m_inBattle = false;

    [field:SerializeField]
    public LocationData LocationData { get; protected set; }

    [SerializeField]
    protected Image m_allyImage;
    [SerializeField]
    protected Image m_enemyImage;

    public AutoBattlerUnit Enemy { get; protected set; }
    public AutoBattlerUnit Hero { get; protected set; }

    [SerializeField]
    protected TextMeshProUGUI m_locationText;

    public void BeginBattle()
    {
        m_inBattle = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_locationText.text = LocationData.LocationName;
        BeginBattle();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_inBattle)
        {
            Hero.UpdateAbilitiesTimer();
            Enemy.UpdateAbilitiesTimer();
        }
    }
}
