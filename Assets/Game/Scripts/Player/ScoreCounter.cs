using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _score = 0;

    public static event Action<int> OnScoreChanged;

    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyKilled -= OnEnemyKilled;
    }

    private void OnEnemyKilled()
    {
        _score++;
        OnScoreChanged(_score);
    }
}
