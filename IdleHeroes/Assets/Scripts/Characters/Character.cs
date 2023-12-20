using MoreMountains.Feedbacks;
using MRF.Dictionary;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public SerializableDictionary<EAbilityEffect, MMFeedbacks> m_feedbacksDictionary = new SerializableDictionary<EAbilityEffect, MMFeedbacks>();

    private void Awake()
    {
        m_feedbacksDictionary.InstantiateDictionary();
    }

    public void ReceiveHit()
    {
        MMFeedbacks value = null;
        if (m_feedbacksDictionary.TryGetValue(EAbilityEffect.Hit, ref value))
        {
            value.PlayFeedbacks();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReceiveHit();
        }
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