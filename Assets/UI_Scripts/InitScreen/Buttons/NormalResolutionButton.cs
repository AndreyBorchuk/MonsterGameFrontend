using UnityEngine;

public class NormalResolutionButton : MonoBehaviour
{
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float buttonHeight = rectTransform.rect.height;
        float buttonWidth = rectTransform.rect.width;
        
        float scale = screenWidth / 1080f;
        rectTransform.sizeDelta = new Vector2(buttonWidth * scale, buttonHeight * scale);
    }

}
