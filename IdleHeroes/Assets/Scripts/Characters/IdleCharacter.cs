using Assets.PixelHeroes.Scripts;
using MoreMountains.Feedbacks;
using MRF.Containers;
using MRF.Dictionary;
using System.Collections.Generic;
using UnityEngine;

public class IdleCharacter : MonoBehaviour
{
    [SerializeField]
    private Character m_character;
    //Temporary TOREMOVE
    public bool m_isPlayer;

    public SerializableDictionary<EAbilityEffect, MMFeedbacks> m_feedbacksDictionary = new SerializableDictionary<EAbilityEffect, MMFeedbacks>();

    [field:SerializeField]
    protected BaseContainerController HealthContainerController { get; set; }

    private void Awake()
    {
        m_feedbacksDictionary.InstantiateDictionary();
    }

    private void Start()
    {
        m_character.SetState(Assets.PixelHeroes.Scripts.AnimationState.Idle);
    }

    public void ReceiveHit(int amount)
    {
        MMFeedbacks value = null;
        if (m_feedbacksDictionary.TryGetValue(EAbilityEffect.Hit, ref value))
        {
            //TODO This should be put in a sequence instead, stop and play causes bugs because delayed feedbacks are NOT stopped when Stop is called
            value?.StopFeedbacks();
            value?.PlayFeedbacks();
            HealthContainerController.Container.RemoveValue(amount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isPlayer && Input.GetKeyDown(KeyCode.Alpha1))
        {
            PerformDamage();
        }
    }

    public void PerformDamage()
    {
        GameManager._Instance.m_enemyCharacter.ReceiveHit(10);
    }
}

public enum EAbilityEffect
{
    Hit,
    Heal,
    Count
}

public struct AbilityFeedbackTuple
{
    public EAbilityEffect Effect;
    public MMFeedback Feedback;
}