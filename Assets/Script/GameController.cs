using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public InventorySystem inventorySystem = new InventorySystem();
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

    public void Save()
    {
        //保存
        saveData.gameData.playerPotion = PlayerInput.playerPotision;
        saveData.gameData.sceneName = SceneController.Instance.CurrentScene;

        saveData.inventoryData.SetInventorySystem(inventorySystem);

        GetSaveSystem().Save(this);
    }

    public void Load()
    {
        //復元
        GetSaveSystem().Load(this);
    }
}
