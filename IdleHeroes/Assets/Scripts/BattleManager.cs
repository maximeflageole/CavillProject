using MoreMountains.Feedbacks;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [SerializeField]
    private MMTimeManager m_timeManager;

    public void BeginAttack()
    {
        m_timeManager.SetTimeScaleTo(0);
    }

    public void StopAttack()
    {
        m_timeManager.SetTimeScaleTo(1);
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.timeScale);
    }
}
