using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class FlagManager
{
    public List<Flag> flags = new List<Flag>();

    public Flag GetFlag(string flagName)
    {
        if (!Exists(flagName)) throw new System.Exception("Exists Flag");

        return flags.FirstOrDefault(x => x.flagName == flagName);
    }

    public bool GetValue(string flagName)
    {
        return GetFlag(flagName).value;
    }

    public void SetFlag(string flagName, bool value)
    {
        Flag flag = GetFlag(flagName);
        flag.value = value;
    }

    public bool Exists(string flagName)
    {
        return flags.Exists(x => x.flagName == flagName);
    }

    public bool Equals(string flagName, bool value)
    {
        return GetValue(flagName) == value;
    }
}

[System.Serializable]
public class Flag
{
    public string flagName;
    public bool value = false;

    public Flag(string flagName, bool value)
    {
        this.flagName = flagName;
        this.value = value;
    }
}