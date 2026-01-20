using UnityEngine;
using UnityEngine.InputSystem;

public class CannonAim : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private Transform _barrel;

    [SerializeField] private float _maxHorizontalDeviation;
    [SerializeField] private float _maxVerticalAngle;
    [SerializeField] private float _minVerticalAngle;

    private float _horizontalInput;
    private float _verticalInput;
    private float _aimSpeed;

    private void Awake()
    {
        _maxVerticalAngle = -_maxVerticalAngle;
        _minVerticalAngle = -_minVerticalAngle;
        _aimSpeed = PlayerPrefs.GetFloat("aim speed", 1f);
    }

    private void Update()
    {
        float horizontalRotate = _horizontalInput * _horizontalSpeed * Time.deltaTime;
        float verticalRotate = -_verticalInput * _verticalSpeed * Time.deltaTime;

        float currentHorizontalRotation = transform.localEulerAngles.y;
        float currentVerticalRotation = _barrel.localEulerAngles.x;

        if (currentHorizontalRotation > 180f)
        {
            currentHorizontalRotation -= 360f;
        }

        if (currentVerticalRotation > 180f)
        {
            currentVerticalRotation -= 360f;
        }

        if (Mathf.Abs(currentHorizontalRotation + horizontalRotate) <= _maxHorizontalDeviation)
        {
            transform.Rotate(0f, horizontalRotate * _aimSpeed, 0f);
        }

        if ((currentVerticalRotation + verticalRotate) <= _minVerticalAngle && 
            (currentVerticalRotation + verticalRotate) >= _maxVerticalAngle)
        {
            _barrel.Rotate(verticalRotate * _aimSpeed, 0f, 0f);
        }
    }

    public void OnHorizontalAim(InputAction.CallbackContext callbackContext)
    {
        _horizontalInput = callbackContext.ReadValue<float>();
    }

    public void OnVerticalAim(InputAction.CallbackContext callbackContext)
    {
        _verticalInput = callbackContext.ReadValue<float>();
    }
}
