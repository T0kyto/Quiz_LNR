using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class DataLoader
{
    public static List<T> GetListFromJSON<T>(string filename)
    {
        if (!File.Exists(filename))
        {
            throw new System.Exception("Wrong JSON filename");
        }

        string json = File.ReadAllText(filename);
        List<T> itemsList = JsonConvert.DeserializeObject<List<T>>(json);

        return itemsList;
    }

    public static string BuildStreamingAssetPath(string assetName)
    {
        return Path.Combine(Application.streamingAssetsPath, assetName);
    }

    public static Sprite LoadImage(string imageName)
    {
        string imagePath = Path.Combine(Application.streamingAssetsPath, imageName);
        Debug.Log(imageName);
        Debug.Log(imagePath);
        if (!File.Exists(imagePath))
        {
            throw new SystemException("Wrong Image path");
        }

        byte[] loadedBytes = File.ReadAllBytes(imagePath);

        Texture2D imageTexture = new Texture2D(1, 1);
        imageTexture.LoadImage(loadedBytes);

        return Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), new Vector2(0.5f, 0.5f));
    }
    
    public static List<T> GetRandomElements<T>(List<T> list, int count)
    {
        if (count >= list.Count)
        {
            return list; 
        }

        List<T> randomElements = new List<T>();
        List<T> copyList = new List<T>(list);

        Random random = new Random();
        for (int i = 0; i < count; i++)
        {
            int index = random.Next(copyList.Count);
            randomElements.Add(copyList[index]);
            copyList.RemoveAt(index);
        }
        return randomElements;
    }
    
    public static void Shuffle<T>(List<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
    
    public static long GetTimeStamp()
    {
        long timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        return timestamp;
    }
}
