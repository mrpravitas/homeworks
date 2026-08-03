using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/Gameplay Config")]
public class GameplayConfig : ScriptableObject
{
    [SerializeField] private float _entitySpawnInterval;
    [SerializeField] private float _entitySpawnRadius;

    public float EntitySpawnIterval => _entitySpawnInterval;
    public float EntitySpawnRadius => _entitySpawnRadius;
}
