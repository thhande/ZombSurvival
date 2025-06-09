using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/save.json";

    public static void SaveHighScore(int score)
    {
        SaveData data = Load();
        if (score > data.highScore)
        {
            data.highScore = score;
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
        }
    }
    public static SaveData Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            return new SaveData(); // if dont have data
        }
    }
}
