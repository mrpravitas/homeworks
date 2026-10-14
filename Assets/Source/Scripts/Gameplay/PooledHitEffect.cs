using UnityEngine;

public class PooledHitEffect : HitEffect, IPoolable
{
    public void OnGet()
    {
        ApplyColor(_baseColor);
    }

    public void OnReturn()
    {
        _lifeTimeRemaining = _lifeTime;
    }

    protected override void Despawn()
    {
        G.PoolManager.ReturnHitEffect(this);
    }
}
