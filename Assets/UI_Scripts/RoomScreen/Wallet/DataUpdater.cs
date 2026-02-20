using System;
using TMPro;
using UnityEngine;


public class DataUpdater : MonoBehaviour
{
    public TMP_Text AmountExpensive;
    public TMP_Text AmountDefault;
    public TMP_Text AmountEnergy;
    public TMP_Text Level;
    string Normal(Int64 amount)
    {
        if (amount >= 1000000000)
        {
            return $"{amount / 1000000000}B";
        }
        if (amount >= 1000000)
        {
            return $"{amount / 1000000}M";
        }
        if (amount >= 1000)
        {
            return $"{amount / 1000}K";
        }
        return $"{amount}";
    }
    void Update()
    {
        var energy = DataHolder.Energy;
        var expensive = DataHolder.CoinsExpensive;
        var defaultCurrency = DataHolder.CoinsDefault;
        var level = DataHolder.Level;

        AmountEnergy.text = Normal(energy);
        AmountExpensive.text = Normal(expensive);
        AmountDefault.text = Normal(defaultCurrency);
        Level.text = $"{level} LVL.";
    }
}
