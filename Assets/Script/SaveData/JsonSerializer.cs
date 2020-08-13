using UnityEngine;
using System.IO;

public static class JsonSerializer
{
    public static void Save(object obj, string fileName)
    {
        //各クラスをjsonにする
#if UNITY_EDITOR
        var json = UnityEditor.EditorJsonUtility.ToJson(obj, true);
#else
        var json = JsonUtility.ToJson(obj, true);
#endif

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
#if UNITY_EDITOR
        UnityEditor.EditorJsonUtility.FromJsonOverwrite(jsonStr, obj);
#else
        JsonUtility.FromJsonOverwrite(jsonStr, obj);
#endif
    }

    private static string GetFilePath(string fileName)
    {
#if UNITY_EDITOR
        string directoryPath = Application.persistentDataPath + "/" + "SaveData_Editor";
#else
        string directoryPath = Application.persistentDataPath + "/" + "SaveData";
#endif

        //ディレクトリが無ければ作成します
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string filePath = directoryPath + "/" + fileName;
        return filePath;
    }
}
