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
            //経験値加算
            GameController.GetParty().Find(alivePlayer.playerData.CharacterID).exp += dropExp;
            //レベルがあがったら体力全回復
            yield return StartCoroutine(BattleMessage.GetWindow().ShowClick(ms));
        }



        //2020/06/13　この下の処理が正しく行われず再度バトルに突入したときステータスが引き継がれない
        Debug.Log("ばぐばぐ");

        //レベルアップ処理
        //基礎ステータスに反映
        //基礎ステータスにバトルで損傷したHPとMPを反映する
        foreach (var item in BattleController.instance.playerCharacters)
        {
            CharacterData characterData = GameController.GetParty().Find(item.playerData.CharacterID);
            Debug.Log(characterData.status.hp);
            characterData.status.hp = item.battleStaus.Status.hp;
            //characterData.status.hp = 777;
            Debug.Log(item.battleStaus.Status.hp);
            characterData.status.mp = item.battleStaus.Status.mp;

            //死亡したキャラはHP１で復活
            if (item.battleStaus.Status.hp < 0)
            {
                characterData.status.hp = 1;
            }
        }
        yield break;
    }
}
