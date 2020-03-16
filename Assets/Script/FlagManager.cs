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
        return flags.FirstOrDefault(x => x.flagName == flagName);
    }

    public bool Get(string flagName)
    {
        if (!Has(flagName)) throw new System.Exception("not flag");
        return GetFlag(flagName).value;
    }

    public void Set(string flagName, bool value)
    {
        if (!Has(flagName)) throw new System.Exception("not flag");
        Flag flag = GetFlag(flagName);
        flag.value = value;
    }

    public bool Has(string flagName)
    {
        Flag flag = GetFlag(flagName);
        if (flag == null) return false;
        return true;
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