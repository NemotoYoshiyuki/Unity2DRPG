using UnityEngine;
using System.IO;

public static class JsonSerializer
{
    public static void Save(object obj, string fileName)
    {
        //各クラスをjsonにする
        var json = JsonUtility.ToJson(obj);
        var filePath = GetFilePath(fileName);
        File.WriteAllText(filePath, json);

        Debug.Log(json);
        Debug.Log("saveFilePath = " + filePath);
    }

    public static void Load(object obj, string fileName)
    {
        string filePath = GetFilePath(fileName);
        if (!File.Exists(filePath))
        {
            Debug.Log(fileName + "はありません！");
            return;
        }
        string jsonStr = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(jsonStr, obj);
    }

    private static string GetFilePath(string fileName)
    {
        string directoryPath = Application.persistentDataPath + "/" + "SaveData";

        //ディレクトリが無ければ作成します
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        string filePath = directoryPath + "/" + fileName;
        return filePath;
    }
}
