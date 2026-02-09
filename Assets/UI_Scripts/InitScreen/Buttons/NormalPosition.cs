using UnityEngine;

public class NormalPosition : MonoBehaviour
{
    public float step;
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float posY = (Screen.height * -1 + rectTransform.rect.height) / 2;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, posY + step);
    }
}
