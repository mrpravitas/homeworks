using System;
using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _lifetime;
    [SerializeField] private float _fadeDuration;

    private bool _isDestroying;
    private bool _wasHit;
    private Renderer _renderer;

    public event Action OnHit;

    private void Start()
    {
        Debug.Log("Target created");
        Destroy(gameObject, _lifetime);

        _renderer = GetComponent<Renderer>();

        AddRandomBehaviour();
    }

    private void Update()
    {
        Debug.Log("Target still alive");
    }

    public void Destroy()
    {
        if (_isDestroying)
        {
            return;
        }

        _wasHit = true;
        _isDestroying = true;

        OnHit.Invoke();

        if (GetComponent<ColorChange>() != null)
        {
            GetComponent<ColorChange>().enabled = false;
        }

        StartCoroutine("Fade");
    }

    private void OnDestroy()
    {
        if (!_wasHit)
        {
            Debug.Log("Target was not hit");
        }
        Debug.Log("Target destroyed");
    }

    private void AddRandomBehaviour()
    {
        int behaviour = UnityEngine.Random.Range(0, 3);

        switch (behaviour)
        {
            case 0:
                gameObject.AddComponent<Scale>();
                break;
            case 1:
                gameObject.AddComponent<Rotate>();
                break;
            case 2:
                gameObject.AddComponent<ColorChange>();
                break;
        }
    }

    private IEnumerator Fade()
    {
        float time = 0f;
        Color color = _renderer.material.color;

        while (time < _fadeDuration)
        {
            float t = time / _fadeDuration;

            Color newColor = color;
            newColor.a = Mathf.Lerp(1f, 0f, t);
            _renderer.material.color = newColor;

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
