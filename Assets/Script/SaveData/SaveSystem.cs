using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem
{
    public const string SaveFileName = "SaveData";

    public bool ExistsSaveData()
    {
        return !File.Exists(SaveFileName);
    }

    public void Save(object obj)
    {
        JsonSerializer.Save(obj, SaveFileName);
    }

    public void Load(object obj)
    {
        JsonSerializer.Load(obj, SaveFileName);
    }

    public void Clear()
    {

    }

    public void Copy()
    {

    }

    public void Delete()
    {

    }
}
