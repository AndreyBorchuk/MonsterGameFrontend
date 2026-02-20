using System;
using UnityEngine;

public class NormalResolutionRS : MonoBehaviour
{
    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        float scaleFactor = Screen.width / 1080f;
        scaleFactor = Math.Min(1.37f, scaleFactor);
        rt.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
    } 
}
