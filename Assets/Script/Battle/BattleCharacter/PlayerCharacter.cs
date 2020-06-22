using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BattleCharacter
{
    public CharacterData characterData;
    public PlayerData playerData;
    public override string CharacterName { get => playerData.CharacterName; set => base.CharacterName = value; }

    public List<Spell> GetSpells()
    {
        return playerData.SpellDatas;
    }

    public List<Skill> GetSkills()
    {
        return playerData.SkillDatas;
    }
}
