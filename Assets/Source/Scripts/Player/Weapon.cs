using UnityEngine;
using Mirror;
using System.Collections;

public class Weapon : NetworkBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private LineRenderer _tracerRenderer;
    [SerializeField] private AudioSource _audioSource;

    private float _lastFireTime;
    private Coroutine _tracerCoroutine;

    private void Awake()
    {
        _lastFireTime = -_fireRate;
    }

    private void Update()
    {
        if (!isOwned)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            CmdShoot(_camera.transform.position, _camera.transform.forward);
        }
    }

    [Command]
    private void CmdShoot(Vector3 origin, Vector3 direction)
    {
        if (Time.time - _lastFireTime <= _fireRate)
        {
            return;
        }

        _lastFireTime = Time.time;

        bool hasHit = Physics.Raycast(origin, direction, out RaycastHit hitInfo, _range);
        Vector3 endPoint = hasHit ? hitInfo.point : origin + direction * _range;

        if (hasHit)
        {
            Health health = hitInfo.collider.GetComponentInParent<Health>();
            if (health != null)
            {
                health.TakeDamage(_damage, netId);
            }
        }

        RpcShotEffect(origin, endPoint);
    }

    [ClientRpc]
    private void RpcShotEffect(Vector3 origin, Vector3 endPoint)
    {
        if (isOwned)
        {
            _audioSource.Play();
        }

        _tracerRenderer.SetPosition(0, origin);
        _tracerRenderer.SetPosition(1, endPoint);

        if (_tracerCoroutine != null)
        {
            StopCoroutine(_tracerCoroutine);
        }
        _tracerCoroutine = StartCoroutine(ShowTracer());
    }

    private IEnumerator ShowTracer()
    {
        _tracerRenderer.enabled = true;
        yield return new WaitForSeconds(_fireRate);
        _tracerRenderer.enabled = false;
    }
}
