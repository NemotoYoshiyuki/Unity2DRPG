using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//戦闘用プレイヤーメンバー
public class PlayerParty : MonoBehaviour
{
    public GameObject playerEntity;
    public List<PlayerCharacter> partyMember;

    public void GeneratePlayer(List<CharacterData> characterDatas)
    {
        foreach (var characterData in characterDatas)
        {
            Join(characterData);
        }
    }

    public PlayerCharacter CreatePlayer(CharacterData playerData)
    {
        GameObject player = Instantiate(playerEntity);

        //初期設定
        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();
        playerCharacter.characterData = playerData;
        playerCharacter.playerData = playerData.playerData;

        //ステータスの設定
        playerCharacter.Initialize(playerData.status);

        //装備の設定
        playerCharacter.battleStaus.equip = playerData.equip;

        //名前の設定
        playerCharacter.CharacterName = playerData.playerData.CharacterName;
        player.name = playerData.playerData.CharacterName;

        playerCharacter.transform.parent = gameObject.transform;
        return playerCharacter;
    }

    //戦闘中仲間の追加
    public void Join(CharacterData characterData)
    {
        PlayerCharacter playerCharacter = CreatePlayer(characterData);
        partyMember.Add(playerCharacter);
    }
}
