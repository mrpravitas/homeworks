using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private Color _hitColor;
    [SerializeField] private GameObject _hitEffectPrefab;
    [SerializeField] private float _hitEffectLifeTime;

    private Transform _transform;
    protected Rigidbody _rigidbody;
    private Renderer _renderer;
    protected Color _defaultColor;
    private float _lifeTimeRemaining;
    private bool _isFlying;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.sharedMaterial.color;
    }

    private void Update()
    {
        _lifeTimeRemaining -= Time.deltaTime;
        if (_lifeTimeRemaining <= 0f)
        {
            Despawn();
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
            SpawnHitEffect(collision.GetContact(0).point);
        }
    }

    public void Launch(Vector3 direction, float speed)
    {
        _transform.forward = direction;
        _rigidbody.velocity = direction * speed;
        _lifeTimeRemaining = _lifeTime;
        _isFlying = true;
    }

    protected void ApplyColor(Color color)
    {
        _renderer.material.color = color;
    }

    private void SpawnHitEffect(Vector3 position)
    {
        GameObject effect = Instantiate(_hitEffectPrefab, position, Quaternion.identity);
        Destroy(effect, _hitEffectLifeTime);
    }

    protected virtual void Despawn()
    {
        Destroy(gameObject);
    }
}
