using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HealthBarColor : MonoBehaviour
{
    public float lowValue = 0.1f;
    public Color lowColor = Color.red;
    public float midValue = 0.5f;
    public Color midColor = Color.yellow;
    public float highValue = 0.9f;
    public Color highColor = Color.green;
    public Image bar;

    // Update is called once per frame
    void Update()
    {
        var value = ((RectTransform)transform).anchorMax.x;
        if (value >= highValue)
            bar.color = highColor;
        else if (value >= midValue)
            bar.color = Color.Lerp(midColor, highColor, (value - midValue) / (highValue - midValue));
        else if (value >= lowValue)
            bar.color = Color.Lerp(lowColor, midColor, (value - lowValue) / (midValue - lowValue));
        else
            bar.color = lowColor;
    }
}
