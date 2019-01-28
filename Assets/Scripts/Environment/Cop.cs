using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cop : MonoBehaviour
{
    Integrity integrity;
    private void Start()
    {
        integrity = FindObjectOfType<Integrity>();
    }
    float time, duration = 1f;
    private void OnTriggerStay(Collider other)
    {
        if (target!=null&&target.transform.Equals(target))
        {
            if (time == 0 || time < Time.time)
            {
                time = Time.time + duration;
                integrity.CopDamage(2);
            }
        }
    }

    bool following;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<House>() && !other.GetComponent<House>().hidden)
        {
            GetComponent<AudioSource>()?.Play();
            target = other.transform;
            following = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (target!=null&&other.transform.Equals(target))
        {
            following = false;
        }
    }


    public NavMeshAgent navMeshAgent;
    public Transform target;

    private void OnValidate()
    {
        if (!navMeshAgent) navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target == null)
        {
            GetComponent<NavMeshRandomPatrol>().enabled = true;
        }
        else
        {
            GetComponent<NavMeshRandomPatrol>().enabled = false;
        }

        if (target)
            if (following || !target.GetComponent<House>().hidden)
                navMeshAgent?.SetDestination(target.position);
            else
                target = null;
    }
}
