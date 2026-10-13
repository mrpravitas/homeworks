using UnityEngine;

public class PooledProjectile : Projectile, IPoolable
{
    public void OnGet()
    {
        ApplyColor(_defaultColor);
    }

    public void OnReturn()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.useGravity = false;
    }

    protected override void Despawn()
    {
        G.PoolManager.ReturnProjectile(this);
    }
}
