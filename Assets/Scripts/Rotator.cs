using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _roationSpeed;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        _transform.Rotate(0f, _roationSpeed * Time.deltaTime, 0f);
    }
}
