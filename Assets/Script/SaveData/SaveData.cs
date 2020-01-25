using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public GameData gameData = new GameData();
    public InventoryData inventoryData = new InventoryData();
    public PartyData partyData = new PartyData();

    [System.Serializable]
    public class GameData
    {
        public Vector2 playerPotion;
        public string sceneName;
        public int money;
    }

    [System.Serializable]
    public class InventoryData
    {
        public InventorySystem inventorySystem;

        public void SetInventorySystem(InventorySystem inventorySystem)
        {
            this.inventorySystem.itemDatas = new List<ItemData>(inventorySystem.itemDatas);
        }
    }

    [System.Serializable]
    public class PartyData
    {
        //メンバー　サブメンバー
        //メンバーのステータス
        public PlayerParty PlayerParty;
        public List<PlayerCharacterData> characterDatas;
    }

    [System.Serializable]
    public class PlayerCharacterData
    {
        public string characterName;
        public PlayerData characterData;
        public Status characterStatus;
    }
}