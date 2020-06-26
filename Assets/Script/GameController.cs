using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    protected static GameController instance;

    [Header("所持金")]
    public int money = 0;
    private const int maxMoney = 99999;
    //最大所持金(規定値:99999999)を返す。

    [Header("ゲーム再開地点")]
    [SceneName] public string resumeScene;
    public Vector3 checkpoint;

    [Header("オプション項目")]
    public int messageSpeed;
    public int soundVolume;
    public int sfxVolume;
    public FlagManager flagManager = new FlagManager();
    public MapFlag mapFlag = new MapFlag();

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

    public static FlagManager GetFlagManager()
    {
        return Instance.flagManager;
    }

    public void Save()
    {
        // //前回のセーブデータを破棄
        // this.saveData = new SaveData();

        // //ゲームデータ保存
        SaveData saveData = SaveSystem.saveData;
        saveData.gameData.playerPotion = PlayerMovement.playerPotision;
        saveData.gameData.sceneName = SceneController.Instance.CurrentScene;

        // //オプション設定保存
        // saveData.gameData.messageSpeed = messageSpeed;
        // saveData.gameData.soundVolume = soundVolume;
        // saveData.gameData.sfxVolume = sfxVolume;

        // //全滅時の再開場所保存
        // saveData.gameData.resumeScene = resumeScene;
        // saveData.gameData.checkpoint = checkpoint;


        // //インベントリデータ保存
        // //saveData.inventoryData.SetInventorySystem(inventorySystem);

        // //パーティーデータ保存
        // var partyMember = Party.GetMember();
        // foreach (var member in partyMember)
        // {
        //     //saveData._partyData.SetData(member);
        // }

        // //フラグデータ保存
        // saveData.flagData.SetFlagData(flagManager.flags);

        // //マップフラグデータ保存
        // saveData.mapFlagData.SetFlagData(mapFlag.mapFlags);

        // //ファイルに書き込む
        // //GetSaveSystem().Save(this);
        _Save();
    }

    public void _Save()
    {
        InventorySystem.Save();
        Party.Save();
        VariablePersister.Save();
        SaveSystem.Save();
    }

    public void _Load()
    {
        SaveSystem.Load();
        Party.Load();
        VariablePersister.Load();
        InventorySystem.Load();
    }

    public void Load()
    {
        //GetSaveSystem().Load(this);
        //PlayerParty.Instance.Load();
        //Party.Load();
        _Load();
    }

    public void GameOver()
    {
        //ペナルティ
        money = money / 2;
        //パーティーの全回復
        //PlayerParty.Instance.FullRecovery();
        //チェックポイントから再開
        SceneController.Instance.Transition(resumeScene, checkpoint);
    }
}
