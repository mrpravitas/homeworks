using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private PooledProjectile _projectilePrefab;
    [SerializeField] private int _projectilePoolSize;

    private Pool<PooledProjectile> _projectilePool;

    private void Awake()
    {
        _projectilePool = new Pool<PooledProjectile>(_projectilePrefab, transform, _projectilePoolSize);
        G.PoolManager = this;
    }

    public PooledProjectile GetProjectile()
    {
        return _projectilePool.Get();
    }

    public void ReturnProjectile(PooledProjectile projectile)
    {
        _projectilePool.Return(projectile);
    }
}
