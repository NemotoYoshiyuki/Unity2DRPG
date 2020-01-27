using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public InventorySystem inventorySystem = new InventorySystem();
    public int money = 0;

    public SaveSystem saveSystem = new SaveSystem();
    public SaveData saveData = new SaveData();

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static InventorySystem GetInventorySystem()
    {
        return instance.inventorySystem;
    }

    public static SaveSystem GetSaveSystem()
    {
        return instance.saveSystem;
    }

    public static SaveData GetSaveData()
    {
        return instance.saveData;
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
        var partyMember = PlayerParty.instance.partyMember;
        foreach (var member in partyMember)
        {
            saveData.partyData.SetData(member.playerData, member.status);
        }

        //ファイルに書き込む
        GetSaveSystem().Save(this);
    }

    public void Load()
    {
        GetSaveSystem().Load(this);
        PlayerParty.instance.Load();
    }
}
