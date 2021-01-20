using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lang
{
    private static string root = "";
    public static Dictionary<string, string> lang = new Dictionary<string, string>();
    public static void Init()
    {
        string txt = FileIO.Read(root + Main.config.lang + ".txt");
        txt = txt.Substring(0, txt.Length - 1);
        txt = txt.Replace("\n", "");
        foreach (string s in txt.Split(';'))
        {
            string[] line = s.Split(':');
            try
            {
                lang.Add(line[0], line[1]);
            }
            catch { }
        }
    }
}
