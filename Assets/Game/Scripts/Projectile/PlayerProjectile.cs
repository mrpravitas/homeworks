public class PlayerProjectile : Projectile
{
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        Destroy(gameObject);
    }
}
