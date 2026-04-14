using UnityEngine;

public class GameStateService
{
    private GameConfig _gameConfig;
    private int _score;
    private GameState _gameState;

    public GameStateService(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
        EventBus.OnGameEvent += HandleEvent;
    }

    public void Init()
    {
        _score = 0;
        _gameState = GameState.Init;
    }

    private void HandleEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType != EventType.ItemPicked)
        {
            return;
        }

        if (_gameState == GameState.Win)
        {
            return;
        }

        if (_gameState == GameState.Init)
        {
            _gameState = GameState.Playing;
        }

        _score++;
        EventBus.Raise(new GameEvent(EventType.ScoreChanged, _score));

        if (_score >= _gameConfig.TargetScore)
        {
            _gameState = GameState.Win;
            EventBus.Raise(new GameEvent(EventType.GameWon));
        }
    }
}
