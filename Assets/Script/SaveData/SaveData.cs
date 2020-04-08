using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public GameData gameData = new GameData();
    public InventoryData inventoryData = new InventoryData();
    public PartyData partyData = new PartyData();
    public FlagData flagData = new FlagData();
    public MapFlagData mapFlagData = new MapFlagData();

    [System.Serializable]
    public class GameData
    {
        public Vector2 playerPotion;
        public string sceneName;
        public int money;

        //オプション
        public int messageSpeed;
        public int soundVolume;
        public int sfxVolume;

        //全滅した時の復活場所
        public string resumeScene;
        public Vector3 checkpoint;
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
            this.characterName = playerData.CharacterName;
            this.characterData = playerData;
            this.characterStatus = status.Copy();
        }
    }

    [System.Serializable]
    public class FlagData
    {
        public List<Flag> flags = new List<Flag>();
        public void SetFlagData(List<Flag> flagDatas)
        {
            this.flags = flagDatas;
        }
    }

    [System.Serializable]
    public class MapFlagData
    {
        public List<Flag> flags = new List<Flag>();
        public void SetFlagData(List<Flag> flagDatas)
        {
            this.flags = flagDatas;
        }
    }
}