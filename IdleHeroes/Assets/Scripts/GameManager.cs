using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    public AutoBattlerTeam PlayerTeam;
    public TeamData DefaultTeamData;

    [field:SerializeField]
    public HubMainPanel HubMainPanel { get; protected set; }

    protected int m_yellowOrbs = 0;
    protected int m_redOrbs = 0;
    protected int m_blueOrbs = 0;
    protected int m_greenOrbs = 0;

    [SerializeField]
    protected TextMeshProUGUI m_yellowOrbsText;
    [SerializeField]
    protected TextMeshProUGUI m_redOrbsText;
    [SerializeField]
    protected TextMeshProUGUI m_blueOrbsText;
    [SerializeField]
    protected TextMeshProUGUI m_greenOrbsText;

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _Instance = this;
    }

    private void Start()
    {
        if (PlayerTeam.AutoBattlerUnits.Count != 0)
        {
            return;
        }
        PlayerTeam.LoadTeam(DefaultTeamData);
        HubMainPanel.TeamListView.Instantiate(PlayerTeam);
    }

    // Update is called once per frame
    void Update()
    {
        m_yellowOrbsText.text = m_yellowOrbs.ToString();
        m_redOrbsText.text = m_redOrbs.ToString();
        m_blueOrbsText.text = m_blueOrbs.ToString();
        m_greenOrbsText.text = m_greenOrbs.ToString();

    }

    public void AddResource(int amount, EResourceType eResourceType)
    {
        switch (eResourceType)
        {
            case EResourceType.Yellow:
                m_yellowOrbs += amount;
                break;
            case EResourceType.Red:
                m_redOrbs += amount;
                break;
            case EResourceType.Blue:
                m_blueOrbs += amount;
                break;
            case EResourceType.Green:
                m_greenOrbs += amount;
                break;
        }
    }
}
