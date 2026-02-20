using UnityEditor;
using UnityEngine;

public class DelPlayerPrefs
{
    [MenuItem("Assets/Del Pref Assets")]
    static void DelAssets()
    {
        PlayerPrefs.DeleteAll();
    }
}
