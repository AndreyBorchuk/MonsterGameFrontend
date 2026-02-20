using UnityEngine;

public class HeaderScaling : MonoBehaviour
{
    public GameObject header;
    void Start()
    {
        float scaleFactor = Screen.width / 1080f;

        RectTransform rt = GetComponent<RectTransform>();
        rt.localScale *= scaleFactor;
        var headerHeight = header.GetComponent<RectTransform>().rect.height;
        rt.position = new Vector3(Screen.width / 2f, Screen.height - headerHeight * scaleFactor / 2f, 0);
    }
}
