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
    [field: SerializeField]
    public Character m_mainCharacter { get; protected set; }
    [field: SerializeField]
    public Character m_enemyCharacter { get; protected set; }

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
}
