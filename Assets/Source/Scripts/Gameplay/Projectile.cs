using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private Material _hitMaterial;
    [SerializeField] private GameObject _hitEffectPrefab;

    private Transform _transform;
    protected Rigidbody _rigidbody;
    private Renderer _renderer;
    private Material _defaultMaterial;
    private float _lifeTimeRemaining;
    private bool _isFlying;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _defaultMaterial = _renderer.sharedMaterial;
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
            ApplyHitMaterial();
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

    protected void ApplyHitMaterial()
    {
        _renderer.sharedMaterial = _hitMaterial;
    }

    protected void ResetMaterial()
    {
        _renderer.sharedMaterial = _defaultMaterial;
    }

    protected virtual void SpawnHitEffect(Vector3 position)
    {
        Instantiate(_hitEffectPrefab, position, Quaternion.identity);
    }

    protected virtual void Despawn()
    {
        Destroy(gameObject);
    }
}
