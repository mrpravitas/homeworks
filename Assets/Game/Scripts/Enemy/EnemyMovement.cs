using System.Collections;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{   
    [SerializeField] private float _stopDistance = 2f;

    private float _updateInterval = 0.33f;
    private NavMeshAgent _agent;
    private Transform _target;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _target = G.PlayerTransform;
        StartCoroutine(Move());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Move()
    {
        yield return null;

        while (true)
        {
            float distance = Vector3.Distance(transform.position, _target.position);

            if (distance <= _stopDistance && HasLineOfSight())
            {
                _agent.ResetPath();
            }
            else
            {
                _agent.SetDestination(_target.position);
            }

            yield return new WaitForSeconds(_updateInterval);
        }
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
