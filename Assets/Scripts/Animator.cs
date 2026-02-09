using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Animator : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _animationSpeed;

    private Image _image;
    private float _switchInterval;
    private int _currentIndex;
    private Coroutine _animationCoroutine;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _switchInterval = 1f / _animationSpeed;
    }

    private void OnEnable()
    {
        _animationCoroutine = StartCoroutine(Animation());
    }

    private void OnDisable()
    {
        StopCoroutine(_animationCoroutine);
        _animationCoroutine = null;
    }

    private IEnumerator Animation()
    {
        while (true)
        {
            _image.sprite = _sprites[_currentIndex];

            _currentIndex = _currentIndex >= _sprites.Length - 1 ? _currentIndex = 0 : _currentIndex + 1;

            yield return new WaitForSeconds(_switchInterval);
        }
    }
}
