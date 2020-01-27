using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerParty : MonoBehaviour
{
    public GameObject playerEntity;
    public List<PlayerCharacter> partyMember;

    public static PlayerParty instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        //セーブデータが存在する時
        if (true)
        {
            //Create();
        }
    }

    public List<PlayerCharacter> AliveMember()
    {
        return partyMember.Where(x => !x.IsDead()).ToList();
    }

    public PlayerCharacter Create(PlayerData playerData)
    {
        GameObject player = Instantiate(playerEntity);
        player.name = playerData.characterName;

        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();
        //playerCharacter.CharacterName = playerData.characterName;
        playerCharacter.status = Instantiate(playerData).status;
        return playerCharacter;
    }

    //新規加入
    public void Join(PlayerData playerData)
    {
        PlayerCharacter playerCharacter = Create(playerData);
        playerCharacter.transform.parent = gameObject.transform;
        partyMember.Add(playerCharacter);
        Debug.Log(playerCharacter.CharacterName + "がパーティーに加入した");
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

        SaveData saveData = GameController.instance.saveData;
        var characterDatas = saveData.partyData.characterDatas;

        foreach (var playerCharacter in characterDatas)
        {
            PlayerCharacter character = Create(playerCharacter.characterData);
            character.playerData = Instantiate(playerCharacter.characterData);
            character.status = playerCharacter.characterStatus.Copy();
            Join(character);
        }
    }
}
