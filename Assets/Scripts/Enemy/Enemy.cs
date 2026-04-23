using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyConfig _enemyConfig;

    private Transform _target;
    private Transform _transform;
    private Rigidbody _rigidbody;
    private IEnemyStrategy _strategy;

    public void Init(Transform target)
    {
        _target = target;
    }

    private void Start()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        CreateStrategy();
    }

    private void FixedUpdate()
    {
        _strategy.Tick(Time.fixedDeltaTime);
    }

    private void CreateStrategy()
    {
        switch (_enemyConfig.StrategyType)
        {
            case EnemyStrategyType.Chase:
            {
                _strategy = new ChaseStrategy(
                    _transform,
                    _rigidbody,
                    _target,
                    _enemyConfig.Speed
                );
                break;
            }
            case EnemyStrategyType.RandomWalk:
            {
                _strategy = new RandomWalkStrategy(
                    _transform,
                    _rigidbody,
                    _enemyConfig.Speed
                );
                break;
            }
        }
    }
}
