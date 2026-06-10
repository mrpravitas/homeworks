using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{   
    [SerializeField] private float _stopDistance = 2f;

    private NavMeshAgent _agent;
    private Transform _target;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _target = G.PlayerTransform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _target.position);

        if (distance <= _stopDistance && HasLineOfSight())
        {
            _agent.ResetPath();
            return;
        }

        _agent.SetDestination(_target.position);
    }

    private bool HasLineOfSight()
    {
        Vector3 origin = transform.position;
        Vector3 targetPos = _target.position;
        Vector3 direction = targetPos - origin;

        if (Physics.Raycast(origin, direction, out RaycastHit hit))
            return hit.transform == _target;

        return false;
    }
}
