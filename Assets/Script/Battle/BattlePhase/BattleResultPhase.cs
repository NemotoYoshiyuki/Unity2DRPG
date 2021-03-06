﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleResultPhase : MonoBehaviour
{
    public IEnumerator Do()
    {
        //お金を獲得
        int gold = BattleController.instance.GetRewardGold();
        GameController.Money += gold;
        if (gold > 0) yield return StartCoroutine(BattleMessage.GetWindow().ShowClick(gold + "ゴールドを てにいれた！"));

        //経験値の獲得とレベルアップ
        List<PlayerCharacter> aliveMember = BattleController.instance.AlivePlayerCharacters;
        int dropExp = BattleController.instance.GetRewardExp();
        if (dropExp > 0)
        {
            foreach (var alivePlayer in aliveMember)
            {
                CharacterData characterData = Party.Find(alivePlayer.playerData.CharacterID);
                string ms = alivePlayer.CharacterName + "は" + dropExp + "けいけんちをかくとく";
                yield return StartCoroutine(BattleMessage.GetWindow().ShowClick(ms));
                int lv = characterData.lv;
                yield return GiveExp(characterData, dropExp);
                if (characterData.lv == lv) continue;
                //レベルが上昇したとき回復する
                alivePlayer.status.hp = characterData.status.maxHp;
                alivePlayer.status.mp = characterData.status.maxMp;
            }
        }

        //アイテムの獲得
        yield return RewardItem();
        yield break;
    }

    private IEnumerator GiveExp(CharacterData characterData, int exp)
    {
        characterData.exp += exp;
        while (IsLevelUp(characterData))
        {
            yield return LevelUp(characterData);
        }
        yield break;
    }

    private bool IsLevelUp(CharacterData characterData)
    {
        int level = characterData.lv;
        int exp = characterData.exp;
        List<int> experienceTable = ExperienceTable.Get();
        if (level >= 99) return false;
        int nextLevel = experienceTable[level + 1];
        if (exp > nextLevel) return true;
        return false;
    }

    private IEnumerator LevelUp(CharacterData characterData)
    {
        List<string> ms = new List<string>();
        Status growth = characterData.playerData.GrowthRate;

        characterData.lv++;
        characterData.status.maxHp += growth.maxHp;
        characterData.status.maxMp += growth.maxMp;
        characterData.status.attack += growth.attack;
        characterData.status.deffence += growth.deffence;
        characterData.status.speed += growth.speed;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append($"{characterData.GetName()}は レベル{characterData.lv}に　あがった!");
        sb.Append($"\nさいだいＨＰが　{growth.maxHp}ポイント　あがった!");
        sb.Append($"\nさいだいMＰが　{growth.maxMp}ポイント　あがった!");
        ms.Add(sb.ToString());

        sb.Clear();
        sb.Append($"こうげき＋{growth.attack}  ぼうぎょ+{growth.deffence}\n");
        sb.Append($"すばやさ＋{growth.speed}");
        ms.Add(sb.ToString());

        yield return BattleMessage.GetWindow().ShowClick(ms);

        //呪文を覚える
        yield return LearnSpell(characterData);
        //特技を覚える
        yield return LearnSkill(characterData);
        yield break;
    }

    private IEnumerator LearnSpell(CharacterData characterData)
    {
        var learnSpells = characterData.playerData.SpellDatas;
        Spell learnSpell = learnSpells.FirstOrDefault(x => x.lv == characterData.lv)?.spell;
        if (learnSpell == null) yield break;
        yield return BattleMessage.GetWindow().ShowClick($"{characterData.GetName()}は　{learnSpell.skillName}の　じゅもんを　おぼえた！");
        yield break;
    }

    private IEnumerator LearnSkill(CharacterData characterData)
    {
        var learnSkills = characterData.playerData.SkillDatas;
        Skill learnSkill = learnSkills.FirstOrDefault(x => x.lv == characterData.lv)?.skill;
        if (learnSkill == null) yield break;
        yield return BattleMessage.GetWindow().ShowClick($"{characterData.GetName()}は　{learnSkill.skillName}の　じゅもんを　おぼえた！");
        yield break;
    }

    private IEnumerator RewardItem()
    {
        Item item = BattleController.instance.GetRewardItem();
        if (item == null) yield break;
        InventorySystem.AddItem(item);
        string ms = "たからばこが　おちている！\n" + item.itemName + "を　てにいれた";
        yield return BattleMessage.GetWindow().ShowClick(ms);
        yield break;
    }
}
