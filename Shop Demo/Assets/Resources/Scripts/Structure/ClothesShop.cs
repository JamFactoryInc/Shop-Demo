using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClothesShop
{
    public static List<string> unsoldClothes = new List<string>();
    public static List<string> selling = new List<string>();
    public static Dictionary<string, GameObject> itemParents = new Dictionary<string, GameObject>();
    public static Mannequin[] mannequins = new Mannequin[4];

    public static void Init()
    {
        foreach(KeyValuePair<string,Clothing> c in Clothing.clothes)
            if(c.Value.price != 0)
                unsoldClothes.Add(c.Value.name);

        GameObject[] MannArray = GameObject.FindGameObjectsWithTag("Mannequin");
        Debug.Log(MannArray.Length);
        for (int i = 0; i < 4; i++)
        {
            SpriteRenderer[] sprites = MannArray[i].GetComponentsInChildren<SpriteRenderer>();
            mannequins[i] = new Mannequin(WithTag(sprites, "MannequinClothes"), WithTag(sprites, "SwapSprite"), MannArray[i].transform.GetChild(7).gameObject);
        }

        EnterShop();
    }

    public static SpriteRenderer[] WithTag(SpriteRenderer[] arr, string tag)
    {
        List<SpriteRenderer> ret = new List<SpriteRenderer>();
        foreach (SpriteRenderer sr in arr)
            if (sr.gameObject.tag.Equals(tag))
                ret.Add(sr);
        return ret.ToArray();
    }

    public static string GetRandom()
    {
        return ShiftToSelling(Random.Range(0, unsoldClothes.Count));
    }

    public static string ShiftToSelling(int index)
    {
        selling.Add(unsoldClothes[index]);
        unsoldClothes.RemoveAt(index);
        return selling[selling.Count - 1];
    }

    public static void AppendSelling()
    {
        unsoldClothes.AddRange(selling);
        selling.Clear();
    }

    public static void EnterShop()
    {
        Debug.Log("Entering shop");
        Restock();
        FillItemValues();
    }

    public static void FillItemValues()
    {
        foreach (KeyValuePair<string, GameObject> kv in itemParents)
        {
            string itemName = kv.Value.GetComponent<ShopItem>().name;  

            //creates a new sprite with the desired texture
            kv.Value.GetComponentInChildren<SpriteRenderer>().sprite = ResourceLoader.clothingTextures[kv.Key];

            //fetches a list of the textmeshes attached to the gameobject
            TextMesh[] ItemPriceName = kv.Value.GetComponentsInChildren<TextMesh>();

            //fetches the clothing item from the Clothing.clothes list
            Clothing clothes = Clothing.clothes[kv.Key];

            //Sets the price
            ItemPriceName[0].text = clothes.price.ToString();

            //
            if (Clothing.clothes[kv.Key].price > Main.save.money)
                ItemPriceName[0].color = Color.red;
            ItemPriceName[1].text = Lang.lang[clothes.name];

            kv.Value.GetComponent<ShopItem>().name = clothes.name;
            Debug.Log(kv.Value.GetComponent<ShopItem>().name);

        }
    }

    public static void Restock()
    {
        foreach (GameObject go in itemParents.Values)
            go.SetActive(true);
        itemParents.Clear();
        GameObject[] itemSpawners = GameObject.FindGameObjectsWithTag("ItemSpawner");
        Debug.Log("Found " + itemSpawners.Length + " Gameobject with the tag 'ItemSpawner'");
        foreach (GameObject go in itemSpawners)
            if (unsoldClothes.Count > 0)
                    itemParents.Add(GetRandom(), go);

        Debug.Log("Randomizing " + selling.Count + " shop items");
    }

    public static void Sell(string clothesID)
    {
        Debug.Log("Player buying " + clothesID);
        int slot = Clothing.clothes[clothesID].article;
        if (Clothing.clothes[Main.save.player.clothes[slot]].price != 0)
        {
            foreach (Mannequin mann in ClothesShop.mannequins)
                if (Clothing.clothes[mann.clothingIDs[slot]].price == 0)
                {
                    selling.Remove(clothesID);
                    mann.Equip(Main.save.player.clothes[slot]);
                    Main.player.Equip(clothesID);
                    return;
                }
            Debug.Log("SLot full");
            return;
        }
        selling.Remove(clothesID);
        Main.player.Equip(clothesID);
        return;



    }

    public static void ExitShop()
    {
        
        AppendSelling();
        Debug.Log("Exiting shop");
    }


}
