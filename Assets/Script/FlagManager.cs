using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class FlagManager : MonoBehaviour
{
    protected static FlagManager instance;
    [SerializeField] private FlagMasterData masterData;
    [SerializeField] private List<Flag> flags = new List<Flag>();

    public static FlagManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            instance = FindObjectOfType<FlagManager>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }

    private static void Create()
    {
        instance = new GameObject("FlagManager").AddComponent<FlagManager>();
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static void Initialize()
    {
        foreach (var item in Instance.masterData.flags)
        {
            Instance.flags.Add(new Flag(item.flagName, false));
        }
    }

    public static Flag GetFlag(string flagName)
    {
        if (!Exists(flagName)) throw new System.Exception("Exists Flag");

        return Instance.flags.FirstOrDefault(x => x.flagName == flagName);
    }

    public static bool GetValue(string flagName)
    {
        return GetFlag(flagName).value;
    }

    public static void SetFlag(string flagName, bool value)
    {
        Flag flag = GetFlag(flagName);
        flag.value = value;
    }

    public static bool Exists(string flagName)
    {
        return Instance.flags.Exists(x => x.flagName == flagName);
    }

    public static bool Equals(string flagName, bool value)
    {
        return GetValue(flagName) == value;
    }

    public static void Save()
    {
        SaveData.FlagData data = SaveSystem.saveData.flagData;
        data.flags = new List<Flag>(Instance.flags);
    }

    public static void Load()
    {
        SaveData.FlagData data = SaveSystem.saveData.flagData;
        Instance.flags = new List<Flag>(data.flags);
    }
}