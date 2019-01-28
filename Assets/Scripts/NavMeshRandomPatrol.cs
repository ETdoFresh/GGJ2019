using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshRandomPatrol : MonoBehaviour
{
    public string startsWith = "Sidewalk";
    public NavMeshAgent navMeshAgent;
    public Waypoint target;
    public float closeEnoughDistance = 1;

    private void OnEnable()
    {
        if (target) navMeshAgent.SetDestination(target.transform.position);
    }

    private void OnValidate()
    {
        if (!navMeshAgent) navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        var closestDistance = Mathf.Infinity;
        foreach (var waypoint in FindObjectsOfType<Waypoint>())
            if (waypoint.name.StartsWith(startsWith))
            {
                var distance = Vector3.Distance(transform.position, waypoint.transform.position);
                if (closestDistance > distance)
                {
                    closestDistance = distance;
                    target = waypoint;
                }
            }
    }

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
        target = target.neighbors[Random.Range(0, target.neighbors.Length)];
        navMeshAgent.SetDestination(target.transform.position);
    }
}
