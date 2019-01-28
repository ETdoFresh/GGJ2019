using System;
using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public float fadeOutDuration = 2;
    public AudioSource audioSource;

    private void OnValidate()
    {
        if (!audioSource) audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOut(fadeOutDuration));
    }

    private IEnumerator FadeOut(float duration)
    {
        var startTime = Time.time;
        var endTime = Time.time + duration;
        var startVolume = audioSource.volume;
        while (Time.time < endTime)
        {
            var t = (Time.time - startTime) / duration;
            audioSource.volume = Mathf.Lerp(startVolume, 0, t);
            yield return null;
        }
        audioSource.volume = 0;
        Destroy(gameObject);
    }
}