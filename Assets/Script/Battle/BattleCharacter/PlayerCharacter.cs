using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCharacter : BattleCharacter
{
    public CharacterData characterData;
    public PlayerData playerData;
    public override string CharacterName { get => playerData.CharacterName; set => base.CharacterName = value; }

    public List<Spell> GetSpells()
    {
        return characterData.GetSpells();
    }

    public List<Skill> GetSkills()
    {
        return characterData.GetSkills();
    }

    public int GetLevel()
    {
        return characterData.lv;
    }

    public int GetExp()
    {
        return characterData.exp;
    }

    public int NextExp()
    {
        //次のレベルアップまでに必要な経験値
        int currentLevel = GetLevel();
        float exp = 10;
        float expMod = 1.2f;
        for (int i = 0; i < currentLevel + 1; i++)
        {
            exp = exp * expMod;
        }
        return (int)exp;
    }
}
