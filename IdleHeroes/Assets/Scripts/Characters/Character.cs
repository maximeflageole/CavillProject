using MoreMountains.Feedbacks;
using MRF.Containers;
using MRF.Dictionary;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Temporary TOREMOVE
    public bool m_isPlayer;

    public SerializableDictionary<EAbilityEffect, MMFeedbacks> m_feedbacksDictionary = new SerializableDictionary<EAbilityEffect, MMFeedbacks>();

    [field:SerializeField]
    protected BaseContainerController HealthContainerController { get; set; }

    private void Awake()
    {
        m_feedbacksDictionary.InstantiateDictionary();
    }

    public void ReceiveHit(int amount)
    {
        MMFeedbacks value = null;
        if (m_feedbacksDictionary.TryGetValue(EAbilityEffect.Hit, ref value))
        {
            value.PlayFeedbacks();
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