using UnityEngine;
using UnityEngine.UI;

public class BattleAbilityUI : MonoBehaviour
{
    [SerializeField]
    protected Image m_abilityImage;
    [SerializeField]
    protected Image m_imagePercentage;

    public void EquipAbility(Sprite sprite)
    {
        gameObject.SetActive(true);
        m_abilityImage.sprite = sprite;
        m_imagePercentage.fillAmount = 1;
    }

    public void UpdateImageFillAmount(float abilityPercentage)
    {
        if (isActiveAndEnabled)
        m_imagePercentage.fillAmount = 1.0f - abilityPercentage;
    }
}
