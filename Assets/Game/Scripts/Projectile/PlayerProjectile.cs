public class PlayerProjectile : Projectile
{
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        collision.gameObject.GetComponent<Health>()?.TakeDamage(1);

        Destroy(gameObject);
    }
}
