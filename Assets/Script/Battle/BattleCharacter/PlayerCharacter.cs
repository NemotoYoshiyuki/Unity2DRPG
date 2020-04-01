using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BattleCharacter
{
    public PlayerData playerData;
    public override string CharacterName { get => playerData.CharacterName; set => base.CharacterName = value; }

    public List<SpellData> GetSpells()
    {
        return playerData.SpellDatas;
    }

    public List<SkillData> GetSkills()
    {
        return playerData.SkillDatas;
    }
}
