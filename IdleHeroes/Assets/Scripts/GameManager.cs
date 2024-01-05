using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    public ABTeam PlayerTeam;
    public TeamData DefaultTeamData;

    [field:SerializeField]
    public HubMainPanel HubMainPanel { get; protected set; }
    [SerializeField]
    private AbilitiesPanel m_abilitiesPanel;

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
        if (PlayerTeam?.ABUnits.Count != 0)
        {
            return;
        }
        PlayerTeam?.LoadTeam(DefaultTeamData);
        HubMainPanel?.TeamListView.Instantiate(PlayerTeam);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Testttt");
        }
    }
}
