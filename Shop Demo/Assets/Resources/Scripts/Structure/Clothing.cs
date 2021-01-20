using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Clothing
{
    public static Dictionary<string, Clothing> clothes = new Dictionary<string, Clothing>();

    public static void Init()
    {
        try
        {
            foreach (Clothing c in Main.config.clothes)
            {
                c.path = c.path.Replace("*", c.name);
                clothes.Add(c.name, c);
            }
        }
        catch
        {

        }

    }

    //List of outfits that this item is a member of
    public List<Outfit> sets = new List<Outfit>();
    public string name;
    public int price;
    //0: Feet, 1: Legs, 2: Torso, 3: Head
    public int article;
    public string path;

    

    public bool IsPartOfOutfit(Outfit o)
    {
        return o.clothes.Where(c => c.Equals(name)).Count() > 0;
    }

}
