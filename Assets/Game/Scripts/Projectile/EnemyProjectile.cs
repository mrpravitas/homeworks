public class EnemyProjectile : Projectile
{
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(1);

        Destroy(gameObject);
    }
}
