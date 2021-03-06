﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public GameData gameData = new GameData();
    public InventoryData inventoryData = new InventoryData();
    public PartyData partyData = new PartyData();
    public FlagData flagData = new FlagData();
    public VariablePersisterData variablePersisterData = new VariablePersisterData();

    [System.Serializable]
    public class GameData
    {
        public float playTime;
        public Vector2 playerPotion;
        public string sceneName;
        public int money;

        //オプション
        public float messageSpeed;
        public float soundVolume;
        public float sfxVolume;

        //全滅した時の復活場所
        public string reStartSceneName;
        public Vector3 reStartpoint;
    }

    [System.Serializable]
    public class InventoryData
    {
        public List<Item> items = new List<Item>();
        public List<Equipment> equipment = new List<Equipment>();
    }

    [System.Serializable]
    public class PartyData
    {
        public List<CharacterData> characterDatas = new List<CharacterData>();
    }

    [System.Serializable]
    public class FlagData
    {
        public List<Flag> flags = new List<Flag>();
    }

    [System.Serializable]
    public class VariablePersisterData
    {
        public List<VariablePersister.VariableData> variableDatas = new List<VariablePersister.VariableData>();
    }
}