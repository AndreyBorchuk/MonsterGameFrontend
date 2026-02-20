using UnityEngine;

public class NormalPoisitonRS : MonoBehaviour
{
    public float offset;
    public bool isRight;

    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        float scaleFactor = Screen.width / 1080f;
        float xPos = isRight ? Screen.width - offset * scaleFactor : offset * scaleFactor;
        rt.anchoredPosition = new Vector2(xPos, rt.anchoredPosition.y);
    }
}
