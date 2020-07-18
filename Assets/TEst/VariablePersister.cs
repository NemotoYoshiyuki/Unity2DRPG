using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class VariablePersister
{
    static List<VariableData> VariableDatas = new List<VariableData>();

    [System.Serializable]
    public class VariableData
    {
        public string key;
        public string variable;

        public VariableData(string key, string variable)
        {
            this.key = key;
            this.variable = variable;
        }
    }

    public static bool Exist(string key)
    {
        return VariableDatas.Any(x => x.key == key);
    }

    static VariableData Get(string key)
    {
        return VariableDatas.FirstOrDefault(x => x.key == key);
    }

    static void Set(string key, object obj)
    {
        bool exists = VariableDatas.Exists(x => x.key == key);

        if (exists == false)
        {
            VariableDatas.Add(new VariableData(key, obj.ToString()));
        }
        else
        {
            Get(key).variable = obj.ToString();
        }
    }

    public static int GetInt(string key)
    {
        return Convert.ToInt32(Get(key).variable);
        //return (int)Get(key).variable;
    }

    public static float GetFloat(string key)
    {
        return Convert.ToSingle(Get(key).variable);
        //return (float)Get(key).variable;
    }

    public static bool GetBool(string key)
    {
        return Convert.ToBoolean(Get(key).variable);
        //return (bool)Get(key).variable;
    }

    public static void SetInt(string key, int value)
    {
        Set(key, value);
    }

    public static void SetFloat(string key, float value)
    {
        Set(key, value);
    }

    public static void SetBool(string key, bool value)
    {
        Set(key, value);
    }

    public static void Save()
    {
        SaveData.VariablePersisterData data = SaveSystem.saveData.variablePersisterData;
        data.variableDatas = new List<VariableData>(VariableDatas);
    }

    public static void Load()
    {
        SaveData.VariablePersisterData data = SaveSystem.saveData.variablePersisterData;
        VariableDatas = new List<VariableData>(data.variableDatas);
    }
}