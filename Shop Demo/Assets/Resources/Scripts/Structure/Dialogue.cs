using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string type;
    public string prompt;
    public string text;
    public Dialogue[] responses;
    public string action;

}
