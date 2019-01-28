using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshAnimator : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public float animationSpeedMultiplier = 1;

    private void Update()
    {
        animator.SetFloat("Forward", navMeshAgent.velocity.magnitude * animationSpeedMultiplier);  
    }
}
