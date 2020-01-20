using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BattleCharacter
{
    public PlayerData playerData;
    public override string CharacterName { get => playerData.characterName; set => base.CharacterName = value; }

    public void Create(PlayerData playerData)
    {
        //プレイヤーデータを元に作成します。主に新規作成で使われます
        PlayerData data = ScriptableObject.Instantiate(playerData);
        playerData = data;
        status = data.status;
    }

    public void Create(SaveData.PlayerCharacterData saveData)
    {
        //セーブデータを元にCharacterを作成します
    }


    public List<SpellData> GetSpells()
    {
        return playerData.spellDatas;
    }

    public List<SkillData> GetSkills()
    {
        return playerData.skillDatas;
    }
}
