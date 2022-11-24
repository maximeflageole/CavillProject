using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocationPanel : MonoBehaviour
{
    protected bool m_inBattle = false;

    [field:SerializeField]
    public LocationData LocationData { get; protected set; }

    [SerializeField]
    protected Image m_allyImage;
    [SerializeField]
    protected Image m_enemyImage;

    public IdleUnit Enemy { get; protected set; }
    public IdleUnit Hero { get; protected set; }

    [SerializeField]
    protected TextMeshProUGUI m_locationText;

    public void BeginBattle()
    {
        m_inBattle = true;

        Hero = new IdleUnit(LocationData.GenerateEnemyData());
        m_allyImage.sprite = Hero.UnitData.UnitSprite;
        GenerateEnemy();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_locationText.text = LocationData.LocationName;
        BeginBattle();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_inBattle)
        {
            Hero.UpdateAttackTimer();
            Enemy.UpdateAttackTimer();
        }
    }

    protected void GenerateEnemy()
    {
        Enemy = new IdleUnit(LocationData.GenerateEnemyData());
        m_enemyImage.sprite = Enemy.UnitData.UnitSprite;
    }
}
