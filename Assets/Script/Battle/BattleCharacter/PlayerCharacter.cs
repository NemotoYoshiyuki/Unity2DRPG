using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BattleCharacter
{
    public PlayerData playerData;
    public override string CharacterName { get => playerData.characterName; set => base.CharacterName = value; }

    public List<SpellData> GetSpells()
    {
        return playerData.spellDatas;
    }

    public List<SkillData> GetSkills()
    {
        return playerData.skillDatas;
    }
}
