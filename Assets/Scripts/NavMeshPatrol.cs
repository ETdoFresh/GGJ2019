using UnityEngine;
using UnityEngine.AI;

public class NavMeshPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public int waypointIndex;
    public Transform waypoint;
    public NavMeshAgent navMeshAgent;
    public float closeEnoughDistance = 1;

    private void Update()
    {
        if (!navMeshAgent.pathPending)
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    OnArrive();
            }
            else if (navMeshAgent.remainingDistance <= closeEnoughDistance)
                OnArrive();
    }

    private void OnArrive()
    {
        SetNextWaypoint();
    }

    public void SetNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        if (waypoint != null)
            waypointIndex = (waypointIndex + 1) % waypoints.Length;

        waypoint = waypoints[waypointIndex];
        navMeshAgent.SetDestination(waypoint.position);
    }
}
