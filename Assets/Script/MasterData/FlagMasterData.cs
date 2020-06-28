using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

[CreateAssetMenu()]
public class FlagMasterData : ScriptableObject
{
    public List<Flag> flags = new List<Flag>();
}
