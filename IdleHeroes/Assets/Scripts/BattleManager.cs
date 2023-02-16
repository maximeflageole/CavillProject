using MoreMountains.Feedbacks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [SerializeField]
    private MMF_Player m_timePlayer;
    private Queue<ABAbility> ActionQueue = new Queue<ABAbility>();
    public bool AbilityInProgress { get; private set; }
    private ABAbility CurrentAction;
    [field:SerializeField]
    public ABTeam PlayerTeam { get; protected set; }
    [field:SerializeField]
    public ABTeam EnemyTeam { get; protected set; }

    //UIs
    [SerializeField]
    private Canvas m_battleResultUI;
    [SerializeField]
    private TextMeshProUGUI m_battleResultText;

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
        while (!AbilityInProgress && ActionQueue.Count != 0)
        {
            if (TryBeginAbility(ActionQueue.Dequeue())) return;
        }
    }

    public void ResetBattle()
    {
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
    }

    public void QueueAbility(ABBattleInstance origin, AbilityData abilityData)
    {
        ActionQueue.Enqueue(new ABAbility(origin, abilityData));
    }

    public bool TryBeginAbility(ABAbility ability)
    {
        if (ability.Origin == null) return false;

        CurrentAction = ability;
        AbilityInProgress = true;
        ability.Origin.BeginAbility();
        m_timePlayer.PlayFeedbacks();
        return true;
    }

    public void OnAbilityAnimationOver()
    {
        ExecuteAbility(CurrentAction.AbilityData, CurrentAction.Origin);
        CurrentAction = null;

        while (ActionQueue.Count != 0)
        {
            if (TryBeginAbility(ActionQueue.Dequeue()))
                return;
        }
        AbilityInProgress = false;
    }

    public void VerifyBattleEnding()
    {
        if (PlayerTeam.ABUnits.Count == 0)
        {
            m_battleResultUI.enabled = true;
            m_battleResultText.text= "Game lost!";
            Debug.Log("game lost");
            return;
        }
        if (EnemyTeam.ABUnits.Count == 0)
        {
            m_battleResultUI.enabled = true;
            m_battleResultText.text = "Game won!";
            Debug.Log("game won");
        }
    }

    private void ExecuteAbility(AbilityData abilityData, ABBattleInstance unit)
    {
        foreach (var effect in abilityData.Effects.actionEffects)
        {
            var target = GetTarget(unit, effect);
            if (target == null)
                continue;

            unit.ExecuteEffect(target, effect.Effect);
        }
    }

    private ABBattleInstance GetTarget(ABBattleInstance unit, SActionEffect effect)
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

    private ABBattleInstance GetUnitAtIndex(ABBattleInstance unit, bool enemyTeam, int index)
    {
        var targetedTeam = EnemyTeam;
        if ((unit.IsInPlayerTeam && !enemyTeam) || (!unit.IsInPlayerTeam && enemyTeam))
        {
            targetedTeam = PlayerTeam;
        }
        return targetedTeam.GetUnitAtIndex(index);
    }

    public float CalculateAutoAttackDamage(ABBattleInstance origin, ABBattleInstance targetUnit)
    {
#if UNITY_EDITOR
        Debug.Log("CalculateAutoAttackDamage in progress");
        Debug.Log(origin.name + " is the origin and has " + origin.GetAABaseDamage() + " base damage");
        Debug.Log(targetUnit.name + " is the target and has " + CalculateDamageMultiplier(targetUnit) + " damage reduction");
        Debug.Log("Autoattack damage is " + origin.GetAABaseDamage() * CalculateDamageMultiplier(targetUnit));
#endif
        return (origin.GetAABaseDamage() * CalculateDamageMultiplier(targetUnit));
    }

    protected float CalculateDamageMultiplier(ABBattleInstance targetUnit)
    {
        return (50f / (50f + Mathf.Max(0, targetUnit.GetArmor())));
    }
}