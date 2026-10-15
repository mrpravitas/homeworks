using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private ScoreUI _scoreUI;

    private int _score;

    private void OnEnable()
    {
        Enemy.OnDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        Enemy.OnDied -= OnEnemyDied;
    }

    private void Update()
    {
        _scoreUI.SetScore(_score);
    }

    private void OnEnemyDied(int points)
    {
        _score += points;
    }
}
