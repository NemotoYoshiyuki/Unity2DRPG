using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandExecutor : MonoBehaviour
{
    public CommandEffectExecutor effectExecutor;

    public IEnumerator Execution(BattleCommand battleCommand)
    {
        //ターン開始処理
        StatusEffect statusEffect = battleCommand.owner.statusEffect;
        if (statusEffect != null) statusEffect.OnActionBefore();

        //行動可能な場合
        if (battleCommand.owner.canAction != false)
        {
            //コマンド実行
            yield return StartCoroutine(battleCommand.Execution());

            //MPのチェック

            //コマンドの効果実行
            yield return effectExecutor.Execution(battleCommand);
        }

       
        //ターンエンド処理
        //全員がおこなう
        if (statusEffect != null)
        {
            statusEffect.OnTurnEnd();
            yield return StartCoroutine(_BattleLogic.Instance.Play());
        }

        //全てのキャラクターの行動が終わったときの処理
        //ステータスの再計算(敵・味方全員)

        //麻痺の解除

        yield break;
    }
}
