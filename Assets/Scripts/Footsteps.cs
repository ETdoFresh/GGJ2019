using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public new Rigidbody rigidbody;
    public new AudioSource audioSource;
    public new AudioClip walkSound;
    public new AudioClip runSound;
    public float prevVelocity;
    public float velocity;
    const float START_SPEED = 0.5f;
    const float MED_SPEED = 8f;

    private void OnDisable()
    {
        audioSource.Stop();
    }

    private void Update()
    {
        if (rigidbody) velocity = rigidbody.velocity.magnitude;

        if (velocity > START_SPEED && prevVelocity <= START_SPEED)
        {
            audioSource.Stop();
            audioSource.clip = walkSound;
            audioSource.Play();
        }
        else if (velocity > MED_SPEED && prevVelocity <= MED_SPEED)
        {
            audioSource.Stop();
            audioSource.clip = runSound;
            audioSource.Play();
        }
        else if (velocity <= START_SPEED && prevVelocity > START_SPEED)
        {
            audioSource.Stop();
        }
        else if (velocity <= MED_SPEED && prevVelocity > MED_SPEED)
        {
            audioSource.Stop();
            audioSource.clip = walkSound;
            audioSource.Play();
        }

        prevVelocity = velocity;
    }
}
