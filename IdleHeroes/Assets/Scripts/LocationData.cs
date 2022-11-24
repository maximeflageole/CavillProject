using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "ScriptableObjects/Location", order = 2)]
public class LocationData : ScriptableObject
{
    public string LocationName;
    public Sprite LocationSprite;

    public List<IdleUnitData> EnemiesList = new List<IdleUnitData>();

    public IdleUnitData GenerateEnemyData()
    {
        if (EnemiesList.Count == 0) return null;

        var enemyIndex = Random.Range(0, EnemiesList.Count);
        return EnemiesList[enemyIndex];
    }
}
