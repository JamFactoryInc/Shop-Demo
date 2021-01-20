using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        switch (col.gameObject.name)
        {
            case "ShopExit":
                ClothesShop.ExitShop();
                ClothesShop.EnterShop();
                break;
            case "ItemParent":
                if (CharacterController.AddMoney(-int.Parse(col.gameObject.GetComponentInChildren<TextMesh>().text)))
                {
                    ClothesShop.Sell(col.GetComponent<ShopItem>().name);
                    col.gameObject.SetActive(false);
                }
                
                break;
            case "Mannequin0":
                InitDialogue(3);
                break;
            case "Mannequin1":
                InitDialogue(2);
                break;
            case "Mannequin2":
                InitDialogue(0);
                break;
            case "Mannequin3":
                InitDialogue(1);
                break;
            case "DialogueTrigger":
                Main.player.inDialogue = true;
                Main.dialogueReader.ReadFromFile(col.tag);
                Main.dialogueReader.ReadFirst();
                break;

        }
    }

    void InitDialogue(int num)
    {
        Main.player.activeMannequin = ClothesShop.mannequins[num];
        foreach (SpriteRenderer sr in Main.player.activeMannequin.swapSprite)
            sr.color = new Color(1, 1, 1, 1);
        Main.player.activeMannequin.swapSprite[Main.player.activeMannequin.selectedSwapSprite].color = Color.cyan;
        Main.player.inDialogue = true;
        Main.player.transform.position = ClothesShop.mannequins[num].playerTeleport.transform.position;
    }
}
