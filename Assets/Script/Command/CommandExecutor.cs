using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandExecutor : MonoBehaviour
{
    public CommandEffectExecutor effectExecutor;

    public IEnumerator Execution(BattleCommand battleCommand)
    {
        //行動開始処理
        //行動キャラが個別に行う
        // StatusEffect statusEffect = battleCommand.owner.statusEffect;
        //  if (statusEffect != null) statusEffect.OnActionBefore();

        //行動可能な場合
        if (battleCommand.owner.canAction != false)
        {
            //コマンド実行
            yield return StartCoroutine(battleCommand.Execution());

            //MPのチェック
            if (!battleCommand.CanEffec()) yield break;
            yield return effectExecutor.Execution(battleCommand);

            //コマンドの効果実行
            //バグ　MP不足など効果が実行できない場合でも効果が実行されてしまう
            //yield return effectExecutor.Execution(battleCommand);
        }


        //ターンエンド処理
        //全員がおこなう
        // if (statusEffect != null)
        // {
        //     statusEffect.OnTurnEnd();
        //     yield return StartCoroutine(_BattleLogic.Instance.Play());
        // }

        //全てのキャラクターの行動が終わったときの処理
        //ステータスの再計算(敵・味方全員)

        //麻痺の解除

        yield break;
    }
}
