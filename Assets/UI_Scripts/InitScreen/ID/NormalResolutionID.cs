using UnityEngine;

public class NormalResolutionID : MonoBehaviour
{
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        float screenWidth = Screen.width;

        float objectHeight = rectTransform.rect.height;
        float objectWidth = rectTransform.rect.width;
        float scale = screenWidth / 1080f;
        rectTransform.sizeDelta = new Vector2(objectWidth * scale, objectHeight * scale);
    }
}
