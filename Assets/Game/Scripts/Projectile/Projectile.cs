using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform _transform;
    private float _speed;
    private int _damage;
    private float _lifeTime;
    private ProjectilePool _pool;

    public int Damage => _damage;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position += transform.forward * (_speed * Time.deltaTime);

        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0f)
        {
            _pool.Return(gameObject);
        }
    }

    public void SetPool(ProjectilePool pool)
    {
        _pool = pool;
    }

    public void Initialize(float lifeTime, float speed, int damage = 1)
    {
        _lifeTime = lifeTime;
        _speed = speed;
        _damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Health>()?.TakeDamage(_damage);

        _pool.Return(gameObject);
    }
}
