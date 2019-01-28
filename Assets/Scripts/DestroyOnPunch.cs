using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPunch : MonoBehaviour
{
    [SerializeField] int gears;
    public int numberOfHits = 2;
    public bool isBeingHit = false;
    public GameObject hitEffect;
    public GameObject destroyEffect;

    Integrity integrity;
    private void Start()
    {
        integrity = FindObjectOfType<Integrity>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isBeingHit || !enabled) return;

        if (other.name.Contains("hand")
            && other.GetComponentInParent<Eater>())
        {
            integrity.CarGear(gears);

            isBeingHit = true;
            numberOfHits--;
            StartCoroutine(HitTimer());

            if (numberOfHits <= 0)
            {
                if (destroyEffect)
                {
                    var gameObject = Instantiate(destroyEffect, transform.position, transform.rotation);
                    Destroy(gameObject, 4);
                }
                Destroy(gameObject);
            }
            else if (hitEffect)
            {
                var gameObject = Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(gameObject, 4);
            }
        }
    }

    private IEnumerator HitTimer()
    {
        yield return new WaitForSeconds(0.5f);
        isBeingHit = false;
    }
}
