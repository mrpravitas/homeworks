using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private Vector3 _axis;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    private Transform _transform;
    private Vector3 _startPosition;

    private void Awake()
    {
        _transform = transform;
        _startPosition = _transform.position;
    }

    private void Update()
    {
        float half = _distance * 0.5f;
        float offset = Mathf.PingPong(Time.time * _speed + half, _distance) - half;
        _transform.position = _startPosition + _axis.normalized * offset;
    }
}
