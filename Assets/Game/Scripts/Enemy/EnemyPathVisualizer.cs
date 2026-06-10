using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPathVisualizer : MonoBehaviour
{
    [SerializeField] private bool _path;

    private LineRenderer _lineRenderer;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _lineRenderer.widthMultiplier = 0.1f;
    }

    private void Update()
    {
        if (_path)
        {
            DrawPath();
        }
    }

    private void DrawPath()
    {
        NavMeshPath path = _navMeshAgent.path;

        _lineRenderer.positionCount = path.corners.Length;
        _lineRenderer.SetPositions(path.corners);
    }
}
