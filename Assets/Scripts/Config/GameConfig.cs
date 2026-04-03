using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _enemySpawnInterval;
    [SerializeField] private float _targetScore;

    public float PlayerSpeed => _playerSpeed;
    public float EnemySpawnInterval => _enemySpawnInterval;
    public float TargetScore => _targetScore;
}
