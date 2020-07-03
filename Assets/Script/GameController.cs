using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    protected static GameController instance;

    [Header("所持金")]
    [SerializeField] private int money = 0;
    private const int maxMoney = 99999;
    public static int Money { get { return Instance.money; } set { Instance.money = Mathf.Clamp(value, 0, maxMoney); } }

    [Header("ゲーム再開地点")]
    [SceneName] public string reStartSceneName;
    public Vector3 reStartpoint;

    [Header("オプション項目")]
    public float messageSpeed = 1;
    public float soundVolume = 1;
    public float sfxVolume = 1;

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

    private void SaveGameData()
    {
        SaveData saveData = SaveSystem.saveData;

        //ゲームデータ保存
        saveData.gameData.playerPotion = PlayerMovement.playerPotision;
        saveData.gameData.sceneName = SceneController.Instance.CurrentScene;
        saveData.gameData.reStartSceneName = reStartSceneName;
        saveData.gameData.reStartpoint = reStartpoint;
        //オプション設定保存
        saveData.gameData.messageSpeed = messageSpeed;
        saveData.gameData.soundVolume = soundVolume;
        saveData.gameData.sfxVolume = sfxVolume;

    }

    public static void Save()
    {
        Instance.SaveGameData();
        InventorySystem.Save();
        Party.Save();
        FlagManager.Save();
        VariablePersister.Save();
        SaveSystem.Save();
    }

    private void LoadGameData()
    {
        SaveData.GameData gameData = SaveSystem.saveData.gameData;

        reStartSceneName = gameData.reStartSceneName;
        reStartpoint = gameData.reStartpoint;

        messageSpeed = gameData.messageSpeed;
        soundVolume = gameData.soundVolume;
        sfxVolume = gameData.sfxVolume;
    }

    public static void Load()
    {
        SaveSystem.Load();
        Instance.LoadGameData();
        Party.Load();
        FlagManager.Load();
        VariablePersister.Load();
        InventorySystem.Load();
    }

    public void GameOver()
    {
        //ペナルティ
        money = money / 2;
        //パーティーの全回復
        //PlayerParty.Instance.FullRecovery();
        //チェックポイントから再開
        SceneController.Transition(reStartSceneName, reStartpoint);
    }
}
