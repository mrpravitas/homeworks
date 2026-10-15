using UnityEngine;

public class ProjectileBoundary : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            Destroy(projectile.gameObject);
        }
    }
}
