using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResultPhase : MonoBehaviour
{
    public IEnumerator Do()
    {
        List<PlayerCharacter> aliveMember = BattleController.instance.AlivePlayerCharacters;
        int dropExp = BattleController.instance.GetRewardExp();

        foreach (var alivePlayer in aliveMember)
        {
            alivePlayer.status.exp = dropExp;
            string ms = alivePlayer.CharacterName + "は" + dropExp + "けいけんちをかくとく";
            yield return StartCoroutine(BattleMessage.GetWindow().ShowClick(ms));

            //経験値加算
            //Party.Find(alivePlayer.playerData.CharacterID).exp += dropExp;
            //テスト
            int lv = Party.Find(alivePlayer.playerData.CharacterID).lv;
            yield return Hoge(Party.Find(alivePlayer.playerData.CharacterID), dropExp);
            if (lv != Party.Find(alivePlayer.playerData.CharacterID).lv)
            {
                //レベルがあがったら体力全回復
                CharacterData data = Party.Find(alivePlayer.playerData.CharacterID);
                alivePlayer.status.hp = data.status.maxHp;
                alivePlayer.status.mp = data.status.maxMp;
            }
        }

        //基礎ステータスにバトルで損傷したHPとMPを反映する
        foreach (var item in BattleController.instance.playerCharacters)
        {
            CharacterData characterData = Party.Find(item.playerData.CharacterID);
            characterData.status.hp = item.battleStaus.Status.hp;
            characterData.status.mp = item.battleStaus.Status.mp;

            //死亡したキャラはHP１で復活
            if (characterData.status.hp < 0)
            {
                characterData.status.hp = 1;
            }
        }
        yield break;
    }

    public IEnumerator Hoge(CharacterData characterData, int exp)
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

    public IEnumerator LevelUp(CharacterData characterData)
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
}
