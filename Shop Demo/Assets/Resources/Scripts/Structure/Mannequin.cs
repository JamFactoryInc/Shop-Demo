using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin
{
    public SpriteRenderer[] clothes;
    public SpriteRenderer[] swapSprite;
    public string[] clothingIDs = new string[4] {"empty_pants", "empty_pants" , "empty_shirt" , "empty_hat" };
    public GameObject playerTeleport;
    public int selectedSwapSprite = 0;


    public Mannequin(SpriteRenderer[] clothes, SpriteRenderer[] swapSprite, GameObject playerTP)
    {
        this.clothes = clothes;
        this.swapSprite = swapSprite;
        playerTeleport = playerTP;
    }
    public string Equip(string clothing)
    {
        try
        {
            int article = Clothing.clothes[clothing].article;
            string ret = clothingIDs[article];
            clothes[article].sprite = ResourceLoader.clothingTextures[clothing];
            clothingIDs[article] = clothing;
            Debug.Log("Replacing slot " + article + " with " + clothing);
            return ret;
        }
        catch
        {
            int slot = Main.player.activeMannequin.selectedSwapSprite + 1;
            ForceEquip("empty", slot);
            return Main.save.player.clothes[slot];
        }
        
    }

    public void ForceEquip(string id, int slot)
    {
        Debug.Log("Forcing! Replacing slot " + slot + " with " + id);
        clothes[slot].sprite = ResourceLoader.clothingTextures[id];
    }

}
