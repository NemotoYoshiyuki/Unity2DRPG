using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResultPhase : MonoBehaviour
{
    public IEnumerator Do()
    {
        //お金を獲得
        int gold = BattleController.instance.GetRewardGold();
        GameController.Money += gold;
        if (gold < 0) yield return StartCoroutine(BattleMessage.GetWindow().ShowClick(gold + "ゴールドを てにいれた！"));

        //経験値の獲得とレベルアップ
        List<PlayerCharacter> aliveMember = BattleController.instance.AlivePlayerCharacters;
        int dropExp = BattleController.instance.GetRewardExp();
        foreach (var alivePlayer in aliveMember)
        {
            CharacterData characterData = Party.Find(alivePlayer.playerData.CharacterID);
            string ms = alivePlayer.CharacterName + "は" + dropExp + "けいけんちをかくとく";
            yield return StartCoroutine(BattleMessage.GetWindow().ShowClick(ms));
            int lv = characterData.lv;
            yield return GiveExp(characterData, dropExp);
            if (characterData.lv < lv) continue;
            alivePlayer.status.hp = characterData.status.maxHp;
            alivePlayer.status.mp = characterData.status.maxMp;
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
        int nextLevel = experienceTable[level + 1];
        if (exp > nextLevel) return true;
        return false;
    }

    private IEnumerator LevelUp(CharacterData characterData)
    {
        List<string> ms = new List<string>();

        characterData.lv++;
        ms.Add(characterData.GetName() + "の レベルがあがった\nレベルが" + characterData.lv + "になった");

        string m = "{0}が{1}あがった";
        Status growth = characterData.playerData.GrowthRate;
        if (growth.maxHp != 0)
        {
            characterData.status.maxHp += growth.maxHp;
            ms.Add(string.Format(m, "体力", growth.maxHp.ToString()));
        }
        if (growth.maxMp != 0)
        {
            characterData.status.maxMp += growth.maxMp;
            ms.Add(string.Format(m, "MP", growth.maxMp.ToString()));
        }
        if (growth.attack != 0)
        {
            characterData.status.attack += growth.attack;
            ms.Add(string.Format(m, "攻撃", growth.attack.ToString()));
        }
        if (growth.deffence != 0)
        {
            characterData.status.deffence += growth.deffence;
            ms.Add(string.Format(m, "防御", growth.deffence.ToString()));
        }
        if (growth.speed != 0)
        {
            characterData.status.speed += growth.speed;
            ms.Add(string.Format(m, "速さ", growth.speed.ToString()));
        }
        yield return BattleMessage.GetWindow().ShowClick(ms);
        yield break;
    }

    private void LearnSpell()
    {

    }

    private void LearnSkill()
    {

    }

    private IEnumerator RewardItem()
    {
        Item item = BattleController.instance.GetRewardItem();
        if (item == null) yield break;
        InventorySystem.AddItem(item);
        string ms = $"たからばこが　おちている！\n" + item + "を　てにいれた";
        yield return BattleMessage.GetWindow().ShowClick(ms);
        yield break;
    }
}
