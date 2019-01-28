
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punchable : MonoBehaviour
{
    [SerializeField] Spawns items;

    [SerializeField] AnimationCurve curve;
    [SerializeField] private AudioClip audioClip;

    [SerializeField] Tutorials tutorials;
    private void OnValidate()
    {
        tutorials = FindObjectOfType<Tutorials>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<House>())
        {
            tutorials.ShowHint(Tutorials.BoxType.punch);
        }

        if (collision.gameObject.tag.Equals("Fist"))
            Punched();
    }

    bool punchable = true;
    private void Punched()
    {
        if (punchable)
        {
            punchable = false;

            var index = Random.Range(0, items.list.Count);
            Generate(items.list[index]);

            var audioSource = GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSecondsRealtime(.5f);
        punchable = true;
    }

    Vector2 targetZ;
    float positionY;
    GameObject itemObj;
    bool transitioning;

    private void Generate(GameObject item)
    {
        var itemObj = Instantiate(item, transform);
        itemObj.transform.localPosition = new Vector3(0, 2f, 0);
        var target = Random.insideUnitCircle * 14f;
        StartCoroutine(Move(itemObj.transform, itemObj.transform.localPosition, target));
    }

    IEnumerator Move(Transform transform, Vector3 pos1, Vector2 target)
    {
        var time = .4f;
        float timer = 0.0f;
        while (timer <= time)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, curve.Evaluate(timer / time), transform.localPosition.z);
            transform.localPosition = Vector3.Lerp(pos1, new Vector3(target.x, transform.localPosition.y, target.y), timer/time);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.GetComponent<Eatable>().fresh = true;
        transform.SetParent(null);
    }
}
