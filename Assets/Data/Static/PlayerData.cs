
using System;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    private static string username;
    private static Int64 energy;
    private static Int64 level;
    private static Int64 coinsExpensive;
    private static Int64 coinsDefault;
    private static string[] team;
    private static Appearance appearance;
    private static ClothesItem[] clothes;
    private static Dictionary<string, Monster> inventory;

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
    public static ClothesItem[] Clothes
    {
        get
        {
            return clothes;
        }
        set
        {
            clothes = value;
        }
    }
    public static Appearance PlayerAppearance
    {
        get
        {
            return appearance;
        }
        set
        {
            appearance = value;
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
