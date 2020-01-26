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
        public InventorySystem inventorySystem = new InventorySystem();

        public void SetInventorySystem(InventorySystem inventorySystem)
        {
            this.inventorySystem.itemDatas = new List<ItemData>(inventorySystem.itemDatas);
        }
    }

    [System.Serializable]
    public class PartyData
    {
        public List<PlayerCharacterData> characterDatas = new List<PlayerCharacterData>();

        public void SetData(PlayerData characterData, Status characterStatus)
        {
            this.characterDatas.Add(new PlayerCharacterData(characterData, characterStatus));
        }
    }

    [System.Serializable]
    public class PlayerCharacterData
    {
        public string characterName;
        public PlayerData characterData;
        public Status characterStatus;

        public PlayerCharacterData(PlayerData playerData, Status status)
        {
            this.characterName = playerData.characterName;
            this.characterData = playerData;
            this.characterStatus = status.Copy();
        }
    }
}