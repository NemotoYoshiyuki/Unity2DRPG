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

    public void Create(SaveData.PartyData playerParty)
    {
        //セーブデータを元に再構成する
    }

    public void Join(PlayerData playerData)
    {
        GameObject player = Instantiate(playerEntity);
        player.name = playerData.characterName;

        PlayerCharacter playerCharacter = GetComponent<PlayerCharacter>();
        playerCharacter.Create(playerData);
    }
}
