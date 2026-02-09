using UnityEngine;

public class Resolution : MonoBehaviour
{
    void Start()
    {
        Camera.main.aspect = (float)Screen.width / Screen.height; 
    }
}
