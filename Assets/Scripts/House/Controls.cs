using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Controls : MonoBehaviour
{
    [SerializeField]UnityEvent punch;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            punch?.Invoke();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            GetComponent<House>().Hide();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            GetComponent<House>().Show();
        }
    }
}
