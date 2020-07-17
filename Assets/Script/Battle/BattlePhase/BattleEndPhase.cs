using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEndPhase : MonoBehaviour
{
    public IEnumerator Win()
    {
        BattleController.isBattle = false;
        yield return SaveStatus();
        yield return StartCoroutine(SceneController.BackToField());
        yield break;
    }

    //データの保存を完全に行うために記録が終了するまで待機する
    private IEnumerator SaveStatus()
    {
        foreach (var item in BattleController.instance.playerCharacters)
        {
            //バトルで損傷したHPとMPを反映する
            CharacterData characterData = Party.Find(item.playerData.CharacterID);
            characterData.status.hp = item.status.hp;
            characterData.status.mp = item.status.mp;

            //#TODO: テスト項目
            //死亡したキャラはHP１で復活
            if (characterData.status.hp < 0)
            {
                characterData.status.hp = 1;
            }
        }
        yield break;
    }

    public IEnumerator GameOver()
    {
        BattleController.isBattle = false;
        yield return StartCoroutine(BattleMessage.GetWindow().ShowClick("まけました"));
        //SceneController.Instance.Transition("Title");
        GameController.Instance.GameOver();
        yield break;
    }
}
