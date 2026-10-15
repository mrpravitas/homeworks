using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform _transform;
    private float _speed;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position += _transform.forward * (_speed * Time.deltaTime);
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
