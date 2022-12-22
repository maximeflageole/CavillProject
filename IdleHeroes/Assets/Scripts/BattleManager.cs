using MoreMountains.Feedbacks;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [SerializeField]
    private MMTimeManager m_timeManager;
    private Queue<AutoBattlerAction> ActionQueue = new Queue<AutoBattlerAction>();
    public bool ActionInProgress { get; private set; }

    public void QueueAction(AutoBattlerUnit origin)
    {
        ActionQueue.Enqueue(new AutoBattlerAction(origin));
    }

    public void BeginAction(AutoBattlerAction action)
    {
        ActionInProgress = true;
        action.Origin.BeginAction();
        m_timeManager.SetTimeScaleTo(0);
    }

    public void StopAction()
    {
        Debug.Log(ActionQueue.Count);
        if (ActionQueue.Count != 0)
        {
            BeginAction(ActionQueue.Dequeue());
            return;
        }
        ActionInProgress = false;
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
        if (!ActionInProgress && ActionQueue.Count != 0)
        {
            BeginAction(ActionQueue.Dequeue());
        }
    }
}