using UnityEngine;

public class TickManager : MonoBehaviour
{
    private EnemySystem _enemySystem;
    private ProjectileSystem _projectileSystem;

    private void Awake()
    {
        _enemySystem = new EnemySystem();
        _projectileSystem = new ProjectileSystem();

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
