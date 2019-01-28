using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshFollow : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform target;

    private void OnValidate()
    {
        if (!navMeshAgent) navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target)
            navMeshAgent?.SetDestination(target.position);
    }
}
