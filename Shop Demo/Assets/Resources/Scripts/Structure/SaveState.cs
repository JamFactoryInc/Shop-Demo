using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
    public Vector2 playerPos;
    public Vector2 cameraPos;
    public int money;
    public int style;
    public Outfit[] mannequins;
    public Outfit player = new Outfit();

    private static string path = "Assets/Resources/save.json";

    public bool Save()
    {

        Debug.Log("Saved config as " + JsonUtility.ToJson(this));
        return FileIO.Write(JsonUtility.ToJson(this), path);
    }

    public bool Load()
    {
        try
        {
            JsonUtility.FromJsonOverwrite(FileIO.Read(path), this);
            Debug.Log("Loaded JSON " + FileIO.Read(path));
            return true;
        }
        catch
        {
            return false;
        }
    }
}
