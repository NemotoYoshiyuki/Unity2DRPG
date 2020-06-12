using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResultPhase : MonoBehaviour
{
    public IEnumerator Do()
    {
        List<PlayerCharacter> aliveMember = PlayerParty.Instance.AliveMember();
        int dropExp = BattleController.instance.GetRewardExp();

        foreach (var alivePlayer in aliveMember)
        {
            alivePlayer.status.exp = dropExp;
            string ms = alivePlayer.CharacterName + "は" + dropExp + "けいけんちをかくとく";
            yield return StartCoroutine(BattleMessage.GetWindow().ShowClick(ms));
        }

        //レベルアップ処理
        //基礎ステータスに反映
        //基礎ステータスにバトルで損傷したHPとMPを反映する
        foreach (var item in aliveMember)
        {
            item.status.hp = item.battleStaus.Status.hp;
            item.status.mp = item.battleStaus.Status.mp;

            //死亡したキャラはHP１で復活
            //レベルがあがったら体力全回復
        }
        yield break;
    }
}
