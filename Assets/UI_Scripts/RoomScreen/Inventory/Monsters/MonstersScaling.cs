using UnityEngine;

public class MonstersScaling : MonoBehaviour
{
    public GameObject Header;
    public GameObject Hat;
    public float step;
    void Start()
    {
        float scaleFactor = Screen.width / 1080f;
        float scaleFactorY = Screen.height / 2340f;

        RectTransform rt = GetComponent<RectTransform>();
        rt.localScale *= scaleFactor;
        var headerHeight = Header.GetComponent<RectTransform>().rect.height;
        var hatPos = Hat.GetComponent<RectTransform>().anchoredPosition.y;
        rt.anchoredPosition = new Vector2(0, Screen.height / 2 - (hatPos + headerHeight - 20) * scaleFactor);
    }
}
