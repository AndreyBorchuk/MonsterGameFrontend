using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public static class DataHolder
{
    private static string version = "1.0.0";
    private static string apiURL = "http://unturned-vanilla.ru:3000";
    private static string resourcesURL = "http://localhost:1000";
    private static string username;
    private static Int64 energy;
    private static Int64 level;
    private static Int64 coinsExpensive;
    private static Int64 coinsDefault;
    private static string[] team;
    private static Dictionary<string, Monster> inventory;
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
    public static Dictionary<string, Monster> Inventory
    {
        get
        {
            return inventory;
        }
        set
        {
            inventory = value;
        }
    }
    public static string[] Team
    {
        get
        {
            return team;
        }
        set
        {
            team = value;
        }
    }

    public static string Username
    {
        get
        {
            return username;
        }
        set
        {
            username = value;
        }
    }

    public static Int64 Energy
    {
        get
        {
            return energy;
        }
        set
        {
            energy = value;
        }
    }

    public static Int64 Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }


    public static Int64 CoinsDefault
    {
        get
        {
            return coinsDefault;
        }
        set
        {
            coinsDefault = value;
        }
    }

    public static Int64 CoinsExpensive
    {
        get
        {
            return coinsExpensive;
        }
        set
        {
            coinsExpensive = value;
        }
    }
}
