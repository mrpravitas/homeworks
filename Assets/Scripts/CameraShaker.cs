using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private Vector3 _startPosition;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _startPosition = _transform.position;
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float shakeTimer = 0f;

        while (shakeTimer < duration)
        {
            shakeTimer += Time.deltaTime;

            Vector2 offset = Random.insideUnitCircle * (magnitude / 10f);
            _transform.position = _startPosition + (Vector3)offset;

            yield return null;
        }

        transform.position = _startPosition;
    }
}
