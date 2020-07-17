using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

//フィールド用
[System.Serializable]
public class Party : MonoBehaviour
{
    [SerializeField] private CharacterMasterData characterMasterData;
    private const int max = 4;
    public List<CharacterData> characterDatas;

    protected static Party instance;
    public static Party Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<Party>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }

    public static Party Create()
    {
        GameObject sceneControllerGameObject = new GameObject("SceneController");
        instance = sceneControllerGameObject.AddComponent<Party>();

        return instance;
    }

    private void Awake()
    {
        //シングルトン
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static void initialize(List<CharacterData> characterDatas)
    {
        Debug.Log(characterDatas.Count);
        Instance.characterDatas = characterDatas;

        //パーティメンバーの作成
        Instance.characterDatas.ForEach(x => Join(x));
    }

    public static List<CharacterData> GetMember()
    {
        //全員
        return Instance.characterDatas;
    }

    //並び順で検索
    public static CharacterData GetMember(int num)
    {
        if (num + 1 > max) return null;
        return Instance.characterDatas[num];
    }

    //IDで検索
    public static CharacterData Find(int id)
    {
        return Instance.characterDatas.Find(x => x.id == id);
    }

    static CharacterData Create(PlayerData playerData)
    {
        return new CharacterData(playerData);
    }

    public void Join(int id)
    {
        //マスタデータから条件にあうものを探す
        PlayerData playerData = characterMasterData.characterData.FirstOrDefault(x => x.CharacterID == id);
        Join(playerData);
    }

    public static void Join(PlayerData playerData)
    {
        Instance.characterDatas.Add(Create(playerData));
    }

    public static void Join(CharacterData characterData)
    {
        //すでにパーティー内に存在する場合
        if (Find(characterData.id) != null) return;
        Instance.characterDatas.Add(characterData);
    }

    public static void FullRecovery()
    {
        foreach (var item in Instance.characterDatas)
        {
            item.FullRecover();
        }
    }

    public static void Save()
    {
        SaveData.PartyData partySaveData = SaveSystem.saveData.partyData;
        partySaveData.characterDatas = new List<CharacterData>(Instance.characterDatas);
    }

    public static void Load()
    {
        SaveData.PartyData partySaveData = SaveSystem.saveData.partyData;

        initialize(partySaveData.characterDatas);
    }

    //入れ替え
    public static void Swap()
    {
        //控えメンバーと入れ替え
    }


    //並び替え
    public static void Sort()
    {
        //メンバーの順番を並び替える
    }

    public static Party Copy()
    {
        return (Party)Instance.MemberwiseClone();
    }
}
