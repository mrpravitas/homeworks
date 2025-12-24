using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
