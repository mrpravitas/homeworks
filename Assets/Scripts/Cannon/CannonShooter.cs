    using UnityEngine;
using UnityEngine.InputSystem;

public class CannonShooter : MonoBehaviour
{
    [SerializeField] private CannonConfig _config;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shotPoint;

    public void OnShoot(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            GameObject projectile = Instantiate(_projectilePrefab, _shotPoint.position, _shotPoint.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * _config.ShotForce);
        }
    }
}
