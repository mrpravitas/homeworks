using System.Collections.Generic;
using UnityEngine;

public class HighSpawnMultiplier : IGameModifier
{
    private IReadOnlyList<IEntityFactory> _factories;
    private ILogger _logger;

    private float[] _previousChances;

    public HighSpawnMultiplier(IReadOnlyList<IEntityFactory> factories, ILogger logger)
    {
        _factories = factories;
        _logger = logger;
    }

    public void OnEnterGameplay()
    {
        _previousChances = new float[_factories.Count];

        for (int i = 0; i < _factories.Count; i++)
        {
            var factory = _factories[i];

            _previousChances[i] = factory.SpawnChance;

            factory.SpawnChance = Mathf.Clamp01(factory.SpawnChance * 2f);
        }

        _logger.Log("Modifier activated: High spawn");
    }

    public void OnExitGameplay()
    {
        for (int i = 0; i < _factories.Count; i++)
        {
            _factories[i].SpawnChance = _previousChances[i];
        }
    }
}
