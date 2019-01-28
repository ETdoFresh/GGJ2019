using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public void SetHealth(float health)
    {
        var percent = Mathf.Clamp01(health / 100);
        var rectTransform = (RectTransform)transform;
        var anchorMaxY = rectTransform.anchorMax.y;
        rectTransform.anchorMax = new Vector2(percent, anchorMaxY);
        rectTransform.offsetMax = Vector2.zero;
    }

    internal void SetColor(Color color)
    {
        //throw new NotImplementedException();
    }
}
