using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private int _targetScore;

    private int _score = 0;

    public static event Action<int> OnScoreChanged;
    public static event Action OnTargetScoreReached;

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
        OnScoreChanged?.Invoke(_score);

        if (_score >= _targetScore)
        {
            OnTargetScoreReached.Invoke();
        }
    }
}
