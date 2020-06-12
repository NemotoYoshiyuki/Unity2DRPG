using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//フィールド用
[System.Serializable]
public class Party
{
    public List<CharacterData> characterDatas;

    private const int max = 4;

    private void Awake()
    {
        //シングルトン
       
    }

    public List<CharacterData> GetMember()
    {
        //全員
        return characterDatas;
    }

    public CharacterData GetMember(int num)
    {
        if (num + 1 > max) return null;
        return characterDatas[num];
    }

    public CharacterData Create(PlayerData playerData)
    {
        return new CharacterData(playerData);
    }

    public void Join(int id)
    {
        //マスタデータから条件にあうものを探す
        //PlayerData playerData = null;
        //CharacterData characterData = new CharacterData(playerData);
        //characterDatas.Add(characterData);

        PlayerData playerData = GameController.Instance.characterMaster.characterData.FirstOrDefault(x => x.CharacterID == id);
        Join(playerData);
    }

    public void Join(PlayerData playerData)
    {
        characterDatas.Add(Create(playerData));
    }

    public void Join(CharacterData characterData)
    {
        //すでにパーティー内に存在する場合
        characterDatas.Add(characterData);
    }

    public void Load()
    {
        //データ読み込み
        if (!GameController.GetSaveSystem().ExistsSaveData())
        {
            Debug.LogWarning("セーブデータが存在しません");
            return;
        }

        SaveData saveData = GameController.Instance.saveData;
        var characterDatas = saveData._partyData.characterDatas;

        foreach (var data in characterDatas)
        {
            Join(data);
        }
    }

    //入れ替え
    public void Swap()
    {
        //控えメンバーと入れ替え
    }


    //並び替え
    public void Sort()
    {
        //メンバーの順番を並び替える
    }

    public Party Copy()
    {
        return (Party)this.MemberwiseClone();
    }
}
