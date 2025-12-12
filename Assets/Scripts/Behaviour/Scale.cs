using UnityEngine;

public class Scale : MonoBehaviour
{
    private float _startScale;
    private float _targetScale;
    private bool _isScaling = true;

    private void Start()
    {
        _startScale = transform.localScale.x;
        _targetScale = _startScale * 2;
    }

    private void Update()
    {
        if (_isScaling)
        {
            transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime;

            if (transform.localScale.x >= _targetScale)
            {
                _isScaling = false;
            }
        }
        else
        {
            transform.localScale -= new Vector3(1f, 1f, 1f) * Time.deltaTime;

            if (transform.localScale.x <= _startScale)
            {
                _isScaling = true;
            }
        }
    }
}
