using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public static class DataHolder
{
    private static string version = "1.0.0";
    private static string apiURL = "http://unturned-vanilla.ru:3000";
    private static string resourcesURL = "http://localhost:1000";
    public static string Version
    {
        get
        {
            return version;
        }
    }
    public static string ApiURL
    {
        get
        {
            return apiURL;
        }
    }
    public static string ResourcesURL
    {
        get
        {
            return resourcesURL;
        }
    }
}
