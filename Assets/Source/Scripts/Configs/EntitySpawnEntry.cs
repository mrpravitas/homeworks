using UnityEngine;

[CreateAssetMenu(fileName = "EntitySpawnEntry", menuName = "Configs/Entity Spawn Entry")]
public class EntitySpawnEntry : ScriptableObject
{
    [SerializeField] private MonoBehaviour _prefab;
    [SerializeField] private ScriptableObject _config;
    [SerializeField] private float _spawnChance;

    public MonoBehaviour Prefab => _prefab;
    public ScriptableObject Config => _config;
    public float SpawnChance => _spawnChance;
}
