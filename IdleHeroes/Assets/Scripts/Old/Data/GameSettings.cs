using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [field:SerializeField]
    public LevelUpsSettings LevelUpsSettings { get; protected set; }

    public static GameSettings _Instance;

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _Instance = this;
    }
}
