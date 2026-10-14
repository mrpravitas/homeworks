using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] protected float _lifeTime;

    protected float _lifeTimeRemaining;

    private Renderer _renderer;
    protected Color _baseColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _baseColor = _renderer.sharedMaterial.color;
        _lifeTimeRemaining = _lifeTime;
    }

    private void Update()
    {
        _lifeTimeRemaining -= Time.deltaTime;

        if (_lifeTimeRemaining <= 0f)
        {
            Despawn();
            return;
        }

        float alpha = _baseColor.a * (_lifeTimeRemaining / _lifeTime);
        ApplyColor(new Color(_baseColor.r, _baseColor.g, _baseColor.b, alpha));
    }

    protected void ApplyColor(Color color)
    {
        _renderer.material.color = color;
    }

    protected virtual void Despawn()
    {
        Destroy(gameObject);
    }
}
