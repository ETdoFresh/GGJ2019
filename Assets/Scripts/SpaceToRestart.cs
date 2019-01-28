using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceToRestart : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButton("Jump"))
            GetComponent<SceneLoader>().LoadScene("MainMenu");
    }
}
