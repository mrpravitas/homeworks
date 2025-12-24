using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifetime;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
