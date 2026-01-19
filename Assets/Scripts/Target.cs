using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Target : MonoBehaviour
{
    [SerializeField] private float _lifetime;

    private bool _isDestroying;
    private Animator _animator;
    private Coroutine _lifetimeCorutine;

    public event Action OnHit;

    private void Start()
    {
        Debug.Log("Target created");

        _animator = GetComponent<Animator>();
        _animator.enabled = false;

        _lifetimeCorutine = StartCoroutine(LifetimeTimer());

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
        _isDestroying = true;

        StopCoroutine(_lifetimeCorutine);

        _animator.enabled = true;
        _animator.SetTrigger("Die");

        OnHit.Invoke();

        if (GetComponent<ColorChange>() != null)
        {
            GetComponent<ColorChange>().enabled = false;
        }
    }

    public void OnDeathAnimationFinished()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (!_isDestroying)
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

    private IEnumerator LifetimeTimer()
    {
        yield return new WaitForSeconds(_lifetime);

        if (!_isDestroying)
        {
            Destroy(gameObject);
        }
    }
}
