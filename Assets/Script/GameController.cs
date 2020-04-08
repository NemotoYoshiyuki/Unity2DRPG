using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("マスターデータ")]
    public CharacterMasterData characterMaster;
    public ItemMasterData itemMasterData;
    public EnemyMasterData enemyMasterData;

    protected static GameController instance;
    public int money = 0;
    [Header("ゲームオーバー再開場所")]
    [SceneName] public string resumeScene;
    public Vector3 checkpoint;

    [Header("オプション項目")]
    public int messageSpeed;
    public int soundVolume;
    public int sfxVolume;

    public InventorySystem inventorySystem = new InventorySystem();
    public FlagManager flagManager = new FlagManager();
    public MapFlag mapFlag = new MapFlag();

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

        //オプション設定保存
        saveData.gameData.messageSpeed = messageSpeed;
        saveData.gameData.soundVolume = soundVolume;
        saveData.gameData.sfxVolume = sfxVolume;

        //全滅時の再開場所保存
        saveData.gameData.resumeScene = resumeScene;
        saveData.gameData.checkpoint = checkpoint;


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

        //マップフラグデータ保存
        saveData.mapFlagData.SetFlagData(mapFlag.mapFlags);

        //ファイルに書き込む
        GetSaveSystem().Save(this);
    }

    public void Load()
    {
        GetSaveSystem().Load(this);
        PlayerParty.Instance.Load();
    }

    public void GameOver()
    {
        //ペナルティ
        money = money / 2;
        //パーティーの全回復
        PlayerParty.Instance.FullRecovery();
        //チェックポイントから再開
        SceneController.Instance.Transition(resumeScene,checkpoint);
    }
}
