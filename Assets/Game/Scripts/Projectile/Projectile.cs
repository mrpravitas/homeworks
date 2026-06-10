using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private Transform _transform;
    private float _speed;

    private void Awake()
    {
        _transform = transform;
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        _transform.position += transform.forward * (_speed * Time.deltaTime);
    }

    public virtual void SetSpeed(float speed) 
    {
        _speed = speed;
    }
}
