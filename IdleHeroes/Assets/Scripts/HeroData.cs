using UnityEngine;

[CreateAssetMenu(fileName = "Hero", menuName = "ScriptableObjects/Hero", order = 1)]
public class HeroData : ScriptableObject
{
    [field: SerializeField]
    public int BaseHealth { get; protected set; } = 100;
    [field: SerializeField]
    public int BaseDamage { get; protected set; } = 10;
}
