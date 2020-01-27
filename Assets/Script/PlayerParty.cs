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

    public PlayerCharacter Create(PlayerData playerData)
    {
        GameObject player = Instantiate(playerEntity);
        player.name = playerData.characterName;

        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();
        PlayerData _playerData = Instantiate(playerData);

        playerCharacter.CharacterName = _playerData.characterName;
        playerCharacter.playerData = _playerData;
        playerCharacter.status = _playerData.status;
        return playerCharacter;
    }

    //新規加入
    public void Join(PlayerData playerData)
    {
        PlayerCharacter playerCharacter = Create(playerData);
        playerCharacter.transform.parent = gameObject.transform;
        partyMember.Add(playerCharacter);
        Debug.Log(playerCharacter.CharacterName + "が新しくパーティーに加入した");
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

        foreach (var data in characterDatas)
        {
            PlayerCharacter playerCharacter = Create(data.characterData);
            playerCharacter.status = data.characterStatus.Copy();
            Join(playerCharacter);
        }
    }
}
