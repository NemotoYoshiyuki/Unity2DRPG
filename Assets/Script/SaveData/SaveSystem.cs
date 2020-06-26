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
        return !File.Exists(SaveFileName);
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

    public SaveData GetSaveData()
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
