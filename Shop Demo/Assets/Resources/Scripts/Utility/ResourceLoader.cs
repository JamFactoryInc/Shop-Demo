using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader
{
    public static Dictionary<string, Sprite> clothingTextures = new Dictionary<string, Sprite>();

    public static void Init()
    {
        foreach (KeyValuePair<string, Clothing> c in Clothing.clothes)
        {
            Object resource = Resources.Load(c.Value.path);
            if (resource)
            {
                (resource as Texture2D).filterMode = FilterMode.Point;
                clothingTextures.Add(c.Value.name, SpriteFromText2D(resource as Texture2D));
                Debug.Log("Loaded Asset " + c.Value.path + " using ID " + c.Value.name);
            }
            else
            {
                Debug.LogError("Failed to load " + c.Value.path);
            }

        }
    }

    public static Sprite SpriteFromText2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(Vector2.zero, Vector2.one * texture.width), Vector2.one / 2, 110 * texture.width / 16);
    }
}
