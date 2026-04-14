using UnityEngine;

[CreateAssetMenu(menuName = "CoinConfig")]
public class CoinConfig : ScriptableObject
{
    [SerializeField] private float _respawnDelay;

    public float RespawnDelay => _respawnDelay;
}
