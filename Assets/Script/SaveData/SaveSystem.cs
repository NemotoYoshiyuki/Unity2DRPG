using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveSystem
{
    public static SaveData saveData = new SaveData();
    public static int fileNumber = -1;
    public const int maxSavefiles = 3;
    public const string SaveFileName = "SaveData";

    //イベントハンドラ
    public Action onSaveComplete;
    public Action onLoadComplete;

    public static bool ExistsSaveData()
    {
        string directoryPath = Application.persistentDataPath + "/" + "SaveData";
        //Directoryが存在するか
        if (Directory.Exists(directoryPath) == false) return false;
        //フィル名SaveData*が存在するか
        string[] files = System.IO.Directory.GetFiles(directoryPath, "SaveData*");
        if (files.Length > 0) return true;
        return false;
    }

    public static bool ExistsSaveFile(int fileNumber)
    {
        string directoryPath = Application.persistentDataPath + "/" + "SaveData" + "/";
        return File.Exists(directoryPath + SaveFileName + fileNumber);
    }

    public static void Save()
    {
        Save(saveData);
    }

    //非同期
    public static void Save(SaveData saveData)
    {
        JsonSerializer.Save(saveData, SaveFileName);
    }

    public static void Save(int fileNumber)
    {
        JsonSerializer.Save(saveData, SaveFileName + fileNumber);
    }

    public static void Load()
    {
        saveData = new SaveData();
        Load(saveData);
    }

    //非同期
    public static void Load(SaveData saveData)
    {
        JsonSerializer.Load(saveData, SaveFileName);
    }

    public static SaveData Load(string filePath)
    {
        saveData = new SaveData();
        JsonSerializer.Load(saveData, filePath);
        return saveData;
    }

    public static SaveData LoadFile(int fileNumber)
    {
        return Load(SaveFileName + fileNumber);
    }

    public static SaveData GetSaveData(int fileNumber)
    {
        SaveData saveData = new SaveData();
        Load(saveData);
        return saveData;
    }

    public SaveData Copy()
    {
        return null;
    }

    public void Delete()
    {

    }
}
