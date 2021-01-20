using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Outfit
{
    public static Dictionary<string, Outfit> sets = new Dictionary<string, Outfit>();

    public static void Init()
    {
        foreach (Outfit o in Main.config.sets)
        {
            sets.Add(o.name, o);
            foreach (string clothing_name in o.clothes)
                Clothing.clothes[clothing_name].sets.Add(o);
        }
    }

    public string name;
    public int style;
    public string[] clothes = new string[4] { "empty_pants", "empty_pants", "empty_shirt", "empty_hat" };

}
