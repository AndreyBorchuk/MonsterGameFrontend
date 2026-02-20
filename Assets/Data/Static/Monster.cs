using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Monster
{
    public string id; // DB ID
    public string name;
    public string monster_id; // Config id, not id in table
    public string rarity;
    public int level;
    public int xp;
    public int atk;
    public int hp;
    public int recovery;
    public string element; 
    public List<string> weapons = new();
    public string active;
    public string passive;
    public string village;
}