using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CannonShooter : MonoBehaviour
{
    [SerializeField] private CannonConfig _config;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shotPoint;

    private int _shotCount = 0;

    public UnityEvent<int> OnShooted;

    public void OnShoot(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            GameObject projectile = Instantiate(_projectilePrefab, _shotPoint.position, _shotPoint.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * _config.ShotForce);

            _shotCount++;
            OnShooted.Invoke(_shotCount);
        }
    }
}
