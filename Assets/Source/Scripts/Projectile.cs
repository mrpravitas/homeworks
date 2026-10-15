using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _destroyZ;

    private Transform _transform;
    private float _speed;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position += _transform.forward * (_speed * Time.deltaTime);

        if (_transform.position.z >= _destroyZ)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.Kill(true);
        }

        Destroy(gameObject);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
