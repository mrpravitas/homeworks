using System;
using System.Text;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    private GameState _currentState;
    private StringBuilder _stringBuilder;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        _stringBuilder = new StringBuilder();
    }

    private void Start()
    {
        _currentState = GameState.Start;
        OnGameStateChanged?.Invoke(_currentState);

        Debug.Log("Game started. First enemy wave will appear soon.");
    }

    private void OnEnable()
    {
        EnemySpawner.OnFirstWaveSpawned += HandleFirstWave;
        ScoreCounter.OnTargetScoreReached += HandleWin;
        PlayerHealth.OnPlayerDeath += HandleLose;
    }

    private void OnDisable()
    {
        EnemySpawner.OnFirstWaveSpawned -= HandleFirstWave;
        ScoreCounter.OnTargetScoreReached -= HandleWin;
        PlayerHealth.OnPlayerDeath -= HandleLose;
    }

    private void HandleFirstWave()
    {
        SetState(GameState.Playing);
    }

    private void HandleWin()
    {
        SetState(GameState.Win);
    }

    private void HandleLose()
    {
        SetState(GameState.Lose);
    }

    private void SetState(GameState newState)
    {
        _currentState = newState;

        _stringBuilder.Clear();
        _stringBuilder.Append("Game state changed to: ");
        _stringBuilder.Append(_currentState);

        Debug.Log(_stringBuilder.ToString());

        OnGameStateChanged?.Invoke(_currentState);
    }
}
