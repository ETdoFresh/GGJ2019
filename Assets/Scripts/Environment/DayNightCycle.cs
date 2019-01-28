using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    float duration = 20;
    [SerializeField] Text dayText;
    float time;
    bool isDay;
    [SerializeField] Material day, night;
    [SerializeField] Sprite sun, moon;
    [SerializeField] Animator animator;
    [SerializeField] Image image;
    [SerializeField] new Light light;
    [SerializeField] Color dayLightColor = Color.white;
    [SerializeField] Color nightLightColor = Color.black;

    public int days;
    void Start()
    {
        dayText = GameObject.FindGameObjectWithTag("Days")?.GetComponent<Text>();
        Change();
        time = Time.time + duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (time < Time.time)
        {
            time = Time.time + (isDay?duration/2f:duration);
            Change();
        }
    }

    private void Change()
    {
        isDay = !isDay;

        days += isDay ? 1 : 0;
        dayText.color = !isDay ? Color.white : Color.black;
        dayText.text = days.ToString();
        animator.SetTrigger("Set");
    }

    public void FinishCycle()
    {
        image.sprite = isDay ? sun : moon;
        animator.SetTrigger("Rise");
        RenderSettings.skybox = isDay ? day : night;
        StartCoroutine(SmoothSetLightColor(isDay ? dayLightColor : nightLightColor));
    }

    public IEnumerator SmoothSetLightColor(Color color)
    {
        if (!light) yield break;

        var duration = 0.3f;
        var startTime = Time.time;
        var endTime = Time.time + duration;
        var startColor = light.color;
        while (Time.time < endTime)
        {
            var t = (Time.time - startTime) / duration;
            light.color = Color.Lerp(startColor, color, t);
            yield return null;
        }
        light.color = color;
    }
}
