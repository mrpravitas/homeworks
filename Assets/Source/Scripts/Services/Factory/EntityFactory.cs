using UnityEngine;

public class EntityFactory<T> : IEntityFactory<T> where T : Object
{
    private T _prefab;
    private ScriptableObject _config;

    public float SpawnChance { get; set; }

    public EntityFactory(T prefab, ScriptableObject config, float spawnChance)
    {
        _prefab = prefab;
        _config = config;
        SpawnChance = spawnChance;
    }

    public void Create(Vector3 position)
    {
        if (Random.value > SpawnChance)
        {
            return;
        }

        T entity = Object.Instantiate(_prefab, position, Quaternion.Euler(90f, 0f, 0f));

        if (entity is IEntityWithConfig entityWithConfig)
        {
            entityWithConfig.Init(_config);
        }
    }
}
