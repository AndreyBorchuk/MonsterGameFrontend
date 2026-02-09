using UnityEngine;

public class BackgroundImage : MonoBehaviour
{
    public GameObject image;
    void Start()
    {
        RectTransform rectTransform = image.GetComponent<RectTransform>();
        
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float screenRatio = screenWidth / screenHeight;
        
        float imageWidth = rectTransform.rect.width;
        float imageHeight = rectTransform.rect.height;
        float imageRatio = imageWidth / imageHeight;
        
        if (screenRatio > imageRatio)
        {
            float scale = screenWidth / imageWidth;
            rectTransform.sizeDelta = new Vector2(imageWidth * scale, imageHeight * scale);
        }
        else
        {
            float scale = screenHeight / imageHeight;
            rectTransform.sizeDelta = new Vector2(imageWidth * scale, imageHeight * scale);
        }
    }
}
