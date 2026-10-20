using UnityEngine;

public class ProjectileBoundary : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            G.ProjectilePool.Release(projectile);
        }
    }
}
