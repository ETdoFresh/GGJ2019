using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Eatable : MonoBehaviour
{
    public float eatDuration = 0.5f;
    public bool fresh;
    private void OnTriggerEnter(Collider collision)
    {
        var eater = collision.GetComponent<Eater>();
        if (eater&&fresh)
        {
            fresh = false;
            eater.Eat(this);
        }
    }

    public void GoIntoMouth(Transform transform)
    {
        if(transform!=null)
        StartCoroutine(GoIntoMouthOverTime(transform));
    }

    public IEnumerator GoIntoMouthOverTime(Transform mouth)
    {
        GetComponent<Collider>().enabled = false;
        var startTime = Time.time;
        var finishTime = Time.time + eatDuration;
        var startPosition = transform.position;
        var startScale = transform.localScale;
        while (Time.time < finishTime)
        {
            var t = (Time.time - startTime) / eatDuration;
            transform.position = Vector3.Lerp(startPosition, mouth.position, t);
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
            yield return null;
        }
        Destroy(gameObject);
    }
}
