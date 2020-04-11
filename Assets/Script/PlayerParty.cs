using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerParty : MonoBehaviour
{
    public GameObject playerEntity;
    public List<PlayerCharacter> partyMember;

    private static PlayerParty instance;

    public static PlayerParty Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<PlayerParty>();

            if (instance != null)
                return instance;

            CreateInstance();

            return instance;
        }
    }

    private static void CreateInstance()
    {
        instance = new GameObject("PlayerParty").AddComponent<PlayerParty>();
        instance.playerEntity = Resources.Load<GameObject>("PlayerEntity");
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<PlayerCharacter> AliveMember()
    {
        return partyMember.Where(x => !x.IsDead()).ToList();
    }

    public PlayerCharacter GetMember(int id)
    {
        return partyMember.FirstOrDefault(x => x.playerData.CharacterID == id);
    }

    public PlayerCharacter Create(PlayerData playerData)
    {
        GameObject player = Instantiate(playerEntity);
        player.name = playerData.CharacterName;

        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();

        playerCharacter.CharacterName = playerData.CharacterName;
        playerCharacter.playerData = playerData;
        playerCharacter.status = playerData.Status.Copy();
        return playerCharacter;
    }

    //新規加入
    public void Join(PlayerData playerData)
    {
        PlayerCharacter playerCharacter = Create(playerData);
        Join(playerCharacter);
    }

    public void Join(int id)
    {
        PlayerData playerData = GameController.Instance.characterMaster.characterData.FirstOrDefault(x => x.CharacterID == id);
        Join(playerData);
    }

    public void Join(PlayerCharacter playerCharacter)
    {
        playerCharacter.transform.parent = gameObject.transform;
        partyMember.Add(playerCharacter);
        Debug.Log(playerCharacter.CharacterName + "がパーティーに加入した");
    }

    public void Load()
    {
        if (!GameController.GetSaveSystem().ExistsSaveData())
        {
            Debug.LogWarning("セーブデータが存在しません");
            return;
        }

        SaveData saveData = GameController.Instance.saveData;
        var characterDatas = saveData.partyData.characterDatas;

        foreach (var data in characterDatas)
        {
            PlayerCharacter playerCharacter = Create(data.characterData);
            playerCharacter.status = data.characterStatus.Copy();
            Join(playerCharacter);
        }
    }

    public void FullRecovery()
    {
        partyMember.ForEach(x => x.Recover(9999));
    }
}
