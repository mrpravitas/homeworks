using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class CannonShooter : MonoBehaviour
{
    [SerializeField] private CannonConfig _config;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shotPoint;

    private int _shotCount = 0;
    private float _timer = 0f;
    private Animator _animator;

    public UnityEvent<int> OnShooted;

    private void Awake()
    {
        if (_config == null)
        {
            Debug.LogError("Cannon config is null");
        }

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
    }

    public void OnShoot(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            if (_timer <= 0f)
            {
                _animator.SetTrigger("Shoot");

                GameObject projectile = Instantiate(_projectilePrefab, _shotPoint.position, _shotPoint.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * _config.ShotForce);

                _shotCount++;
                PlayerPrefs.SetInt("shot count", PlayerPrefs.GetInt("shot count", 0) + 1);
                OnShooted.Invoke(_shotCount);

                _timer = _config.ReloadTime;
            }
            else
            {
                Debug.LogWarning("On reload");
            }
        }
    }
}
