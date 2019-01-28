using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Tutorials))]
public class Integrity : MonoBehaviour
{
    float health, max=100;

    float deathRate = 2,deathTime;
    float rodentDamage = 5;
    float repairValue = .5f;

    GameObject peopleUI, ToiletUI, FoodUI;
    [SerializeField]GameObject ratIcon, timeIcon, repairIcon, copIcon, gearIcon;
    float ratAlert, timeAlert, repairAlert, copAlert,gearAlert, duration = 1;

    [SerializeField] Tutorials tutorials;
    [SerializeField] AudioSource sfx;
    private void OnValidate()
    {
        tutorials = GetComponent<Tutorials>();
        sfx = GetComponent<AudioSource>();
    }

    private void Start()
    {
        UpdateHealth(max);
        deathTime = Time.time + deathRate;
    }
    private void Update()
    {
        if (deathTime < Time.time)
        {
            deathTime = Time.time + deathRate;
            timeAlert = Time.time + duration;
            UpdateHealth(-deathRate);
        }

        if (timeAlert > 0 && timeAlert > Time.time)
        {
            timeIcon.SetActive(true);
            tutorials.ShowHint(Tutorials.BoxType.time);
        }
        else
        {
            timeIcon.SetActive(false);
        }

        if (copAlert > 0 && copAlert > Time.time)
        {
            copIcon.SetActive(true);
            tutorials.ShowHint(Tutorials.BoxType.cop);
        }
        else
        {
            copIcon.SetActive(false);
        }

        if (ratAlert > 0 && ratAlert > Time.time)
        {
            ratIcon.SetActive(true);
            tutorials.ShowHint(Tutorials.BoxType.rat);
        }
        else
        {
            ratIcon.SetActive(false);
        }

        if (repairAlert > 0 && repairAlert > Time.time)
        {
            repairIcon.SetActive(true);
            tutorials.ShowHint(Tutorials.BoxType.people);
        }
        else
        {
            repairIcon.SetActive(false);
        }

        if (gearAlert > 0 && gearAlert > Time.time)
        {
            gearIcon.SetActive(true);
            tutorials.ShowHint(Tutorials.BoxType.gears);
        }
        else
        {
            gearIcon.SetActive(false);
        }
    }

    [SerializeField] GameObject main, gameover;
    [SerializeField] Text finaldays;
    void UpdateHealth(float value)
    {
        health += value;

        if (health <= 0)
        {
            health = 0;
            Time.timeScale = 0;
            var days = GameObject.FindGameObjectWithTag("Days")?.GetComponent<Text>().text;
            main.SetActive(false);
            gameover.SetActive(true);
            finaldays.text = days + " Days";
            StartCoroutine(FillList(int.Parse(days)));
        }
        else
        {
            FindObjectOfType<HealthBar>().SetHealth(health);
            FindObjectOfType<HealthBar>().SetColor(value > 0 ? Color.green : Color.red);
        }   
    }

    [SerializeField] GameObject listitem, list;
    private IEnumerator FillList(int days)
    {
        var items = new List<string>()
        {
            "Clogged Toilets",
            "Dirty Sinks",
            "Expired Milk",
            "Too Much Trash",
            "Roach Nests",
            "Bed Wetters",
            "Too Much Poop to Handle",
            "OCD",
            "Electric Bills",
            "Broken Pipes",
            "Cold Showers",
            "Puberty",
            "Random Acts of God",
            "Intentional Acts of God",
            "Psychopath Roommates",
            "Neighborhood SerialKiller",
            "SerialKiller Killed the Neighborhood Watch",
            "Accidently Killed the Neighbor's Dog",
            "Preexisting Conditions",
            "Horses are no Inside Pets",
            "Creepy Roommate with a Python",
            "Too Many Dad Jokes",
            "Hashtag Millenials",
            "Did they just assume my Gender?",
            "Forgot which cup was poisoned",
            "Loud Honeymooners",
            "Light Bulbs don't go there",
            "Obesity",
            "Late for AA Meetings",
            "Forgot the AA Meeting",
            "Mistook Sex AA for AA",
            "It was fun though"
        };

        for (int i = 0; i < days && i<items.Count; i++)
        {
            GameObject item = Instantiate(listitem, list.transform);
            item.GetComponentInChildren<Text>().text = items[i];
            sfx.Play();
            yield return new WaitForSecondsRealtime(.4f);
        }
    }

    [SerializeField]Text people, toilets, food;
    internal void UpdateValues(int people, int toilets, int food)
    {
        this.people.text = people.ToString();
        this.toilets.text = toilets.ToString();
        this.food.text = food.ToString();
    }

    internal void CarGear(float v)
    {
        gearAlert = Time.time + duration;
        UpdateHealth(v);
    }

    internal void CopDamage(float v)
    {
        copAlert = Time.time + duration;
        UpdateHealth(-v);
    }
    internal void RodentDamage(int v)
    {
        ratAlert = Time.time + duration;
        UpdateHealth(-v * rodentDamage);
    }

    internal void Repair(int people)
    {
        repairAlert = Time.time + duration;
        UpdateHealth(people * repairValue);
    }

 
}
