using UnityEngine;
using UnityEngine.InputSystem;

public class CannonShooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private float _shotForce;

    public void OnShoot(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            GameObject projectile = Instantiate(_projectilePrefab, _shotPoint.position, _shotPoint.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * _shotForce);
        }
    }
}
