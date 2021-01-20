using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueReader : MonoBehaviour
{
    public static string root = "Dialogue/";
    public Dialogue activeDialogue = new Dialogue();
    public Text dialogueText;
    public Text[] prompts;
    public GameObject dialgogueBox;
    public List<string> talkedTo;

    public void ReadFromFile(string name)
    {
        if (!talkedTo.Contains(name))
        {
            talkedTo.Add(name);
            JsonUtility.FromJsonOverwrite(FileIO.Read(root + name + "First.json"), activeDialogue);
        }
        else
            JsonUtility.FromJsonOverwrite(FileIO.Read(root + name + "Subsequent.json"), activeDialogue);
    }

    public void ReadFirst()
    {
        dialgogueBox.SetActive(true);
        dialogueText.text = activeDialogue.text;
        for (int i = 0; i < 3; i++)
            if (activeDialogue.responses.Length > i)
                prompts[i].text = activeDialogue.responses[i].prompt;
            else
                prompts[i].text = "";
    }

    public void ReadFirstResponse()
    {
        activeDialogue = activeDialogue.responses[0];
        ReadFirst();
    }

    public void ReadSecondResponse()
    {
        activeDialogue = activeDialogue.responses[1];
        ReadFirst();
    }

    public void ReadThirdResponse()
    {
        activeDialogue = activeDialogue.responses[2];
        ReadFirst();
    }

    public void OptionOne()
    {
        SelectOption(0);
    }

    public void OptionTwo()
    {
        SelectOption(1);
    }

    public void OptionThree()
    {
        SelectOption(2);
    }

    private void SelectOption(int opt)
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (activeDialogue.responses.Length > opt)
            Invoke(activeDialogue.responses[opt].action, 0);
    }

    public void SellHat()
    {
        string id = Main.save.player.clothes[3];
        CharacterController.AddMoney(CalcSellPrice(id));
        Main.player.Equip("empty_hat");
    }
    public void SellShirt()
    {
        string id = Main.save.player.clothes[2];
        CharacterController.AddMoney(CalcSellPrice(id));
        Main.player.Equip("empty_shirt");
    }

    public void SellPants()
    {
        string id = Main.save.player.clothes[1];
        CharacterController.AddMoney(CalcSellPrice(id));
        Main.player.Equip("empty_pants");
    }

    public int CalcSellPrice(string id)
    {
        return Mathf.FloorToInt(Clothing.clothes[id].price * 0.75f);
    }

    public void Exit()
    {
        Main.player.inDialogue = false;
        dialgogueBox.SetActive(false);
    }
}
