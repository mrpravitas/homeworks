using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class EntityFactory : IEntityFactory
{
    private readonly IObjectResolver _resolver;
    private readonly MonoBehaviour _prefab;
    private readonly ScriptableObject _config;

    public float SpawnChance { get; set; }

    public EntityFactory(IObjectResolver resolver, MonoBehaviour prefab, ScriptableObject config, float spawnChance)
    {
        _resolver = resolver;
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

        var entity = _resolver.Instantiate(_prefab, position, Quaternion.Euler(90f, 0f, 0f));

        if (entity is IEntityWithConfig entityWithConfig)
        {
            entityWithConfig.Init(_config);
        }
    }
}
