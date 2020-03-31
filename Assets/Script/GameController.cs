using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("マスターデータ")]
    public CharacterMasterData characterMaster;
    public ItemMasterData itemMasterData;

    protected static GameController instance;
    public int money = 0;
    public InventorySystem inventorySystem = new InventorySystem();
    public FlagManager flagManager = new FlagManager();

    public SaveSystem saveSystem = new SaveSystem();
    public SaveData saveData = new SaveData();

    public static GameController Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            instance = FindObjectOfType<GameController>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
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

    private static void Create()
    {
        instance = new GameObject("GameController").AddComponent<GameController>();
    }

    public static InventorySystem GetInventorySystem()
    {
        return Instance.inventorySystem;
    }

    public static SaveSystem GetSaveSystem()
    {
        return Instance.saveSystem;
    }

    public static FlagManager GetFlagManager()
    {
        return Instance.flagManager;
    }

    public static SaveData GetSaveData()
    {
        return Instance.saveData;
    }

    public void Save()
    {
        //前回のセーブデータを破棄
        this.saveData = new SaveData();

        //ゲームデータ保存
        saveData.gameData.playerPotion = PlayerInput.playerPotision;
        saveData.gameData.sceneName = SceneController.Instance.CurrentScene;

        //インベントリデータ保存
        saveData.inventoryData.SetInventorySystem(inventorySystem);

        //パーティーデータ保存
        var partyMember = PlayerParty.Instance.partyMember;
        foreach (var member in partyMember)
        {
            saveData.partyData.SetData(member.playerData, member.status);
        }

        //フラグデータ保存
        saveData.flagData.SetFlagData(flagManager.flags);

        //ファイルに書き込む
        GetSaveSystem().Save(this);
    }

    public void Load()
    {
        GetSaveSystem().Load(this);
        PlayerParty.Instance.Load();
    }
}
