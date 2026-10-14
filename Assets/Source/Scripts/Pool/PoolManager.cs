using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private PooledProjectile _projectilePrefab;
    [SerializeField] private int _projectilePoolSize;
    [SerializeField] private PooledHitEffect _hitEffectPrefab;
    [SerializeField] private int _hitEffectPoolSize;

    private Pool<PooledProjectile> _projectilePool;
    private Pool<PooledHitEffect> _hitEffectPool;

    private void Awake()
    {
        _projectilePool = new Pool<PooledProjectile>(_projectilePrefab, transform, _projectilePoolSize);
        _hitEffectPool = new Pool<PooledHitEffect>(_hitEffectPrefab, transform, _hitEffectPoolSize);
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

    public PooledHitEffect GetHitEffect()
    {
        return _hitEffectPool.Get();
    }

    public void ReturnHitEffect(PooledHitEffect effect)
    {
        _hitEffectPool.Return(effect);
    }
}
