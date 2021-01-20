using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileIO
{
    public static bool Write(string s, string path)
    {
        Debug.Log("Accessing file " + Application.streamingAssetsPath+"/" + path);
        try
        {
            File.WriteAllText(Application.streamingAssetsPath + "/" + path, s);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static string Read(string path)
    {
        Debug.Log("Accessing file " + Application.streamingAssetsPath+"/" + path);
        try
        {
            StreamReader reader = new StreamReader(Application.streamingAssetsPath + "/" + path);
            string s = reader.ReadToEnd();
            reader.Close();
            return s;
        }
        catch
        {
            return "Err";
        }
    }
}
