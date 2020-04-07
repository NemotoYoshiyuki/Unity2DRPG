using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class MapFlag
{
    public List<Flag> mapFlags = new List<Flag>();

    public void Init()
    {

    }

    public Flag GetFlag(string flagName)
    {
        //if (!Exists(flagName)) throw new System.Exception("Exists Flag");

        return mapFlags.FirstOrDefault(x => x.flagName == flagName);
    }

    public bool GetValue(string flagName)
    {
        return GetFlag(flagName)?.value ?? false;
    }

    public void SetFlag(string flagName, bool value)
    {
        Flag flag = GetFlag(flagName);
        if (flag == null)
        {
            mapFlags.Add(new Flag(flagName, value));
            return;
        }
        flag.value = value;
    }

    public bool Exists(string flagName)
    {
        return mapFlags.Exists(x => x.flagName == flagName);
    }

    public bool Equals(string flagName, bool value)
    {
        return GetValue(flagName) == value;
    }
}