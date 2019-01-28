using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Belly : MonoBehaviour
{
    List<string> eaten = new List<string>();

    bool rodent
    {
        get
        {
            return mice > poison;
        }
    }
    bool hungry
    {
        get
        {
            return people  > food* 3;
        }
    }
    bool poop
    {
        get
        {
            return people  > toilets* 3;
        }
    }
    bool happy
    {
        get
        {
            return people>0&& !poop && !hungry;
        }
    }
    int people
    {
        get
        {
            return eaten.Count(z => z.Equals("People"));
        }
    }
    int toilets
    {
        get
        {
            return eaten.Count(z => z.Equals("Toilet"));
        }
    }
    int food
    {
        get
        {
            return eaten.Count(z => z.Equals("Oven") || z.Equals("Fridge"));
        }
    }
    int mice
    {
        get
        {
            return eaten.Count(z => z.Equals("Rat"));
        }
    }
    int poison
    {
        get
        {
            return eaten.Count(z => z.Equals("Poison"));
        }
    }

    public void Add(string item)
    {
        eaten.Add(item);
    }

    float rodentTime, repairTime, rodentInterval = 10, repairInterval=2;

    [SerializeField]Integrity integrity;
    private void Update()
    {
        if (rodent && (rodentTime==0||rodentTime<Time.time))
        {
            rodentTime = Time.time + rodentInterval;
            integrity.RodentDamage(mice-poison);
        }
        if (happy && (repairTime == 0 || repairTime < Time.time)){
            repairTime = Time.time + repairInterval;
            integrity.Repair(people);
        }
        integrity.UpdateValues(people, toilets, food);
    }
}
