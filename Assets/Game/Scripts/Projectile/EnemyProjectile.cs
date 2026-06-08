public class EnemyProjectile : Projectile
{
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        Destroy(gameObject);
    }
}
