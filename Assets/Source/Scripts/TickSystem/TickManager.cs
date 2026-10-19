using UnityEngine;

public class TickManager : MonoBehaviour
{
    [SerializeField] private float _nearBoundary;
    [SerializeField] private float _farBoundary;

    private TickSystem _enemySystem;
    private TickSystem _projectileSystem;

    private void Awake()
    {
        _enemySystem = new TickSystem(_nearBoundary, _farBoundary);
        _projectileSystem = new TickSystem(_nearBoundary, _farBoundary);

        G.EnemySystem = _enemySystem;
        G.ProjectileSystem = _projectileSystem;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        _enemySystem.Tick(deltaTime);
        _projectileSystem.Tick(deltaTime);
    }
}
