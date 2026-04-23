using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private EnemyStrategyType _strategyType;

    public float Speed => _speed;
    public EnemyStrategyType StrategyType => _strategyType;
}