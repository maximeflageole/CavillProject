using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitDetailsPanel : MonoBehaviour
{
    [SerializeField]
    protected Image m_characterImage;
    [SerializeField]
    protected TextMeshProUGUI m_characterName;
    [SerializeField]
    protected TextMeshProUGUI m_levelText;
    [SerializeField]
    protected StatsPanel m_statsPanel;
    [SerializeField]
    protected Sprite m_lockedSprite;
    [SerializeField]
    protected List<Image> m_abilityImages = new List<Image>();
    [SerializeField]
    protected List<GameObject> m_abilityLevelUnlockTexts = new List<GameObject>();
    [SerializeField]
    protected List<Image> m_equipmentImages = new List<Image>();
    [SerializeField]
    protected List<GameObject> m_equipmentLevelUnlockTexts = new List<GameObject>();

    public ABUnit ABUnit { get; protected set; }

    public void Initialize(ABUnit unit)
    {
        ABUnit = unit;
        m_statsPanel.SetStats(unit.UnitDynamicData.GetCurrentStatsTuples());
        m_characterImage.sprite = unit.UnitStaticData.UnitSprite;
        m_characterName.text = unit.UnitStaticData.UnitName;
        m_levelText.text = "Lvl. " + unit.UnitDynamicData.Level.ToString();

        var i = 0;
        foreach (var equipmentSlot in unit.UnitDynamicData.m_equipmentSlots)
        {
            if (!equipmentSlot.IsUnlocked)
            {
                m_equipmentImages[i].sprite = m_lockedSprite;
                m_equipmentLevelUnlockTexts[i].gameObject.SetActive(true);
            }
            else
            {
                m_equipmentImages[i].sprite = null;
                m_equipmentLevelUnlockTexts[i].gameObject.SetActive(false);
            }
            i++;
        }

        i = 0;
        foreach (var abilitySlot in unit.UnitDynamicData.m_abilitySlots)
        {
            if (!abilitySlot.IsUnlocked)
            {
                m_abilityImages[i].sprite = m_lockedSprite;
                m_abilityLevelUnlockTexts[i].gameObject.SetActive(true);
            }
            else
            {
                m_abilityImages[i].sprite = null;
                m_abilityLevelUnlockTexts[i].gameObject.SetActive(false);
            }
            i++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActiveAndEnabled && ABUnit != null)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        ABUnit.LevelUp();
        Initialize(ABUnit);
    }
}