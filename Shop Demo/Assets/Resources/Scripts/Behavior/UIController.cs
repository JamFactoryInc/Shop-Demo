using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static Text moneyText;
    public static Text styleText;
    public static GameObject menuOverlay;

    private void Start()
    {
        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        //styleText = GameObject.Find("StyleText").GetComponent<Text>();
        menuOverlay = GameObject.Find("Menu");
        menuOverlay.SetActive(false);
    }

    public static void UpdateMoney()
    {
        moneyText.text = Main.save.money.ToString();
    }

    public static void OpenMenu()
    {
        Time.timeScale = 0;
        menuOverlay.SetActive(true);
    }

    public static void CloseMenu()
    {
        Time.timeScale = 1;
        menuOverlay.SetActive(false);
    }
}
