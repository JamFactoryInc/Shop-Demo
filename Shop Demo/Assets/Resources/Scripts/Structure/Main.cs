using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Config config;
    public static SaveState save;
    public static Animator animator;
    public static CharacterController player;
    public static DialogueReader dialogueReader;

    static Main()
    {
        save = new SaveState();
        config = new Config();
        config.Load();
        //config.Save();
        Lang.Init();
        Clothing.Init();
        Outfit.Init();
    }
    

    void Start()
    {
        ResourceLoader.Init();
        ClothesShop.Init();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<CharacterController>();
        dialogueReader = GameObject.Find("Player").GetComponent<DialogueReader>();
    }

    private void Update()
    {
        ToggleTimeScale();
        try
        {
            if (player.inDialogue)
                player.SwapWithMannequin();
        }
        catch { }
        
    }

    

    public static void Quit()
    {
        Application.Quit();
    }

    void ToggleTimeScale()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (!player.inDialogue)
                if (Time.timeScale == 1) UIController.OpenMenu(); else UIController.CloseMenu();
            else
            {
                try
                {
                    player.inDialogue = false;
                    dialogueReader.dialgogueBox.SetActive(false);
                    foreach (SpriteRenderer sr in player.activeMannequin.swapSprite)
                        sr.color = new Color(1, 1, 1, 0);
                    
                }
                catch { }
            }
    }

}

