using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class GameplayState : IUpdatableState
{
    private IMessageBus _bus;
    private IGameplayInput _gameplayInput;
    private IGameplayHud _gameplayHud;
    private GameplayConfig _config;

    private IReadOnlyList<IEntityFactory> _factories;
    private IReadOnlyList<IGameModifier> _modifiers;

    private float _timer;

    public GameplayState(IMessageBus bus, IGameplayInput gameplayInput, 
        GameplayConfig config, IReadOnlyList<IEntityFactory> factories, 
        IReadOnlyList<IGameModifier> gameModifiers, IGameplayHud hud)
    {
        _bus = bus;
        _gameplayInput = gameplayInput;
        _config = config;
        _factories = factories;
        _modifiers = gameModifiers;
        _gameplayHud = hud;
    }

    public void Enter()
    {
        Time.timeScale = 1f;
        _gameplayInput.SetPauseHandler(OnPause);

        for (int i = 0; i < _modifiers?.Count; i++)
        {
            _modifiers[i].OnEnterGameplay();
        }

        _gameplayHud.ShowModifiers(_modifiers);
    }

    public void Exit()
    {
        _gameplayInput.SetPauseHandler(null);

        for (int i = 0; i < _modifiers?.Count;  ++i)
        {
            _modifiers[i].OnExitGameplay();
        }

        _gameplayHud.HideModifiers();
    }

    public void Tick(float deltaTime)
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        _timer += deltaTime;

        if (_timer >= _config.EntitySpawnIterval)
        {
            _timer = 0f;

            Vector3 position = GetRandomPosition();

            for (int i = 0; i < _factories.Count; i++)
            {
                _factories[i].Create(position);
            }
        }
    }

    private void OnPause()
    {
        _bus.Publish(new ChangeStateSignal(typeof(PauseState)));
    }

    private Vector3 GetRandomPosition()
    {
        float radius = _config.EntitySpawnRadius;
        float x = Random.Range(-radius, radius); 
        float z = Random.Range(-radius, radius);
        return new Vector3(x, 0f, z);
    }
}
