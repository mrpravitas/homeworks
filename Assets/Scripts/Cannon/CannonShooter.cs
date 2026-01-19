using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CannonShooter : MonoBehaviour
{
    [SerializeField] private CannonConfig _config;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shotPoint;

    private int _shotCount = 0;
    private float _timer = 0f;

    public UnityEvent<int> OnShooted;

    private void Awake()
    {
        if (_config == null)
        {
            Debug.LogError("Cannon config is null");
        }
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
