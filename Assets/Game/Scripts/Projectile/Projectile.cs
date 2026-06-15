using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private Transform _transform;
    private float _speed;
    private int _damage;

    private void Awake()
    {
        _transform = transform;
        _damage = 1;
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        _transform.position += transform.forward * (_speed * Time.deltaTime);
    }

    public void SetSpeed(float speed) 
    {
        _speed = speed;
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Health>()?.TakeDamage(_damage);

        Destroy(gameObject);
    }
}
