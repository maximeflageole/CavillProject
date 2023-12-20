using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField]
    protected List<TextMeshProUGUI> m_statNameTexts = new List<TextMeshProUGUI>();
    [SerializeField]
    protected List<TextMeshProUGUI> m_statAmountTexts = new List<TextMeshProUGUI>();

    public void SetStats(List<StatTuple> statTuples)
    {
        for (var i = 0; i < m_statNameTexts.Count; i++)
        {
            if (statTuples.Count < i)
            {
                m_statNameTexts[i].gameObject.SetActive(false);
                m_statAmountTexts[i].gameObject.SetActive(false);
                continue;
            }
            m_statNameTexts[i].gameObject.SetActive(true);
            m_statAmountTexts[i].gameObject.SetActive(true);
            m_statNameTexts[i].text = statTuples[i].PrimaryStatistic.ToString();
            m_statAmountTexts[i].text = statTuples[i].Value.ToString("#.#");
        }
    }
}

[System.Serializable]
public struct StatTuple
{
    public EPrimaryStatistic PrimaryStatistic;
    public float Value;
}