using UnityEngine;

public class DestructibleObject : Health
{
    private void OnTriggerEnter(Collider collider)
    {
        Projectile projectile = collider.GetComponent<Projectile>();

        if (projectile != null)
        {
            TakeDamage(projectile.Damage);
        }
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
