using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;

    private int _score;

    private void OnEnable()
    {
        Enemy.OnDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        Enemy.OnDied -= OnEnemyDied;
    }

    private void OnEnemyDied(int points)
    {
        _score += points;
        OnScoreChanged?.Invoke(_score);
    }
}
