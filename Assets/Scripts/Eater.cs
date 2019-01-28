using Invector.CharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Eater : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Belly belly;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip eatSound;
    [SerializeField] private Transform mouth;

    private void OnValidate()
    {
        belly = GetComponent<Belly>();
        this.animator = GetComponent<Animator>();
    }

    private void Start()
    {
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSecondsRealtime(5);
        eating = false;
    }

    bool eating;
    List<Eatable> food = new List<Eatable>();

    public void Eat(Eatable eatable)
    {
        food.Add(eatable);

        if (!eating)
        {
            eating = true;
            StartCoroutine(Reset());
            GetComponent<House>().DisableInput();
            animator.SetTrigger("Eat");
            belly.Add(eatable.gameObject.tag);
            if (audioSource)
            {
                audioSource.Stop();
                audioSource.clip = eatSound;
                audioSource.Play();
            }
        }
    }

    public void Devour()
    {
        while (food.Count > 0)
        {
            food.First().GoIntoMouth(mouth ? mouth : transform);
            food.RemoveAt(0);
        }
        eating = false;
    }
}
