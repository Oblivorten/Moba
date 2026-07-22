using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementComponent : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 destination)
    {
        if (!_agent.isOnNavMesh) {
            return;
        }

        _agent.isStopped = false;
        _agent.SetDestination(destination);
    }

    public void Stop()
    {
        if (!_agent.isOnNavMesh) {
            return;
        }

        _agent.isStopped = true;
        _agent.ResetPath();
    }

    public bool HasReachedDestination()
    {
        if (!_agent.isOnNavMesh)
        {
            return false;
        }

        if (_agent.pathPending)
        {
            return false;
        }

        if (!_agent.hasPath)
        {
            return false;
        }

        return _agent.remainingDistance <= _agent.stoppingDistance;
    }
}