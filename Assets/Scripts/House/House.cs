using Invector.CharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Collider[] fists;
    [SerializeField] AudioClip punch;
    [SerializeField] vThirdPersonController input;
    [SerializeField] List<GameObject> body;
    [SerializeField] GameObject self;
    [SerializeField] AudioSource source;
    private void OnValidate()
    {
        source = GetComponent<AudioSource>();
        this.animator = GetComponent<Animator>();
        input = GetComponent<vThirdPersonController>();
        self = GameObject.FindGameObjectWithTag("House");
        body=GameObject.FindGameObjectsWithTag("Body").ToList();
        fists = GameObject.FindGameObjectsWithTag("Fist").Select(z => z.GetComponent<Collider>()).ToArray();
    }

    private void Start()
    {

        originalY = self.transform.localPosition.y;
    }
    public void ActivatePunch()
    {
        foreach (var fist in fists)
        {
            fist.enabled = true;
        }
    }

    public void DeactivatePunch()
    {
        foreach (var fist in fists)
        {
            fist.enabled = false;
        }
    }

    internal void EnableInput()
    {
        input.lockMovement = false;
    }

    internal void DisableInput()
    {
        input.lockMovement = true;
        input.input = Vector2.zero;
    }

    public void Punch()
    {
        DisableInput();
        source.Stop();
        source.clip = punch;
        source.Play();
        animator.SetTrigger("Punch");
    }

    public void Hide()
    {
        DisableInput();
        foreach (var item in body)
        {
            item.SetActive(false);
        }
        animator.enabled = false;
        hidden = true;
        StartCoroutine(Sit());
    }

    public void Show()
    {
        StartCoroutine(GetUp());
    }

    public bool hidden;
    float originalY, floorY= -76f;
    [SerializeField] AnimationCurve hidingCurve;
    IEnumerator Sit()
    {
        var time = .4f;
        float timer = 0.0f;
        while (timer <= time)
        {
            self.transform.localPosition = Vector3.Lerp(new Vector3(self.transform.localPosition.x, originalY, self.transform.localPosition.z),
                new Vector3(self.transform.localPosition.x, floorY, self.transform.localPosition.z), hidingCurve.Evaluate(timer / time));
            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator GetUp()
    {
        var time = .4f;
        float timer = 0.0f;
        while (timer <= time)
        {
            self.transform.localPosition = Vector3.Lerp(new Vector3(self.transform.localPosition.x, floorY, self.transform.localPosition.z),
                new Vector3(self.transform.localPosition.x, originalY, self.transform.localPosition.z), hidingCurve.Evaluate(timer / time));
            timer += Time.deltaTime;
            yield return null;
        }
        animator.enabled = true;
        EnableInput();
        foreach (var item in body)
        {
            item.SetActive(true);
        }
        hidden = false;
    }

}
