using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private Color _hitColor;

    private Transform _transform;
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private float _lifeTimeRemaining;
    private bool _isFlying;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        _lifeTimeRemaining -= Time.deltaTime;
        if (_lifeTimeRemaining <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isFlying)
        {
            return;
        }

        _isFlying = false;
        _rigidbody.useGravity = true;

        if (collision.collider.GetComponent<TargetMover>() != null)
        {
            ApplyColor(_hitColor);
        }
    }

    public void Launch(Vector3 direction, float speed)
    {
        _transform.forward = direction;
        _rigidbody.velocity = direction * speed;
        _lifeTimeRemaining = _lifeTime;
        _isFlying = true;
    }

    private void ApplyColor(Color color)
    {
        _renderer.material.color = color;
    }
}
