using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Config
{
    public float movementSpeed;
    public Clothing[] clothes;
    public Outfit[] sets;
    public string lang;
    private static string path =  "config.json";

    public bool Save()
    {
        Debug.Log("Saved config as " + JsonUtility.ToJson(this));
        return FileIO.Write( JsonUtility.ToJson(this), path);
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

