using MoreMountains.Feedbacks;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [SerializeField]
    private MMF_Player m_timePlayer;
    private Queue<AutoBattlerAction> ActionQueue = new Queue<AutoBattlerAction>();
    public bool ActionInProgress { get; private set; }
    private AutoBattlerAction CurrentAction;
    [field:SerializeField]
    public AutoBattlerTeam PlayerTeam { get; protected set; }
    [field:SerializeField]
    public AutoBattlerTeam EnemyTeam { get; protected set; }

    public void QueueAbility(AutoBattlerUnit origin, AbilityData abilityData)
    {
        ActionQueue.Enqueue(new AutoBattlerAction(origin, abilityData));
    }

    public bool TryBeginAction(AutoBattlerAction action)
    {
        if (action.Origin == null) return false;

        CurrentAction = action;
        ActionInProgress = true;
        action.Origin.BeginAbility();
        m_timePlayer.PlayFeedbacks();
        return true;
    }

    public void StopAction()
    {
        ExecuteAbility(CurrentAction.AbilityData, CurrentAction.Origin);
        CurrentAction = null;

        while (ActionQueue.Count != 0)
        {
            if (TryBeginAction(ActionQueue.Dequeue()))
                return;
        }
        ActionInProgress = false;
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
        while (!ActionInProgress && ActionQueue.Count != 0)
        {
            if (TryBeginAction(ActionQueue.Dequeue())) return;
        }
    }

    private void ExecuteAbility(AbilityData abilityData, AutoBattlerUnit unit)
    {
        foreach (var effect in abilityData.Effects.actionEffects)
        {
            var target = GetTarget(unit, effect);
            if (target == null)
                continue;

            unit.ExecuteEffect(target, effect.Effect);
        }
    }

    private AutoBattlerUnit GetTarget(AutoBattlerUnit unit, SActionEffect effect)
    {
        foreach (var targetType in effect.TargetTypes)
        {
            switch (targetType)
            {
                case ETargetType.Melee:
                    return GetUnitAtIndex(unit, true, 0);
                case ETargetType.Self:
                    return unit;
                case ETargetType.Ally:
                    break;
                case ETargetType.Count:
                    break;
                default:
                    break;
            }
        }
        return null;
    }

    private AutoBattlerUnit GetUnitAtIndex(AutoBattlerUnit unit, bool enemyTeam, int index)
    {
        var targetedTeam = EnemyTeam;
        if ((unit.IsInPlayerTeam && !enemyTeam) || (!unit.IsInPlayerTeam && enemyTeam))
        {
            targetedTeam = PlayerTeam;
        }
        return targetedTeam.GetUnitAtIndex(index);
    }
}