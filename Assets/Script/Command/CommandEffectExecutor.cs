using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//バトルコマンドの効果を処理するクラスです
public class CommandEffectExecutor : MonoBehaviour
{

    public IEnumerator Execution(BattleCommand battleCommand)
    {
        BattleCharacter owner = battleCommand.owner;
        List<BattleCharacter> targets = battleCommand.target;
        TargetUnit targetUnit = battleCommand.GetTargetType().targetUnit;

        var effects = battleCommand.GetEffect();

        //効果の範囲が全体の場合
        if (battleCommand.GetTargetType().targetRange == TargetRange.全体)
        {
            _BattleLogic.Instance.AnimationPlay(battleCommand.GetCommand().animation);
        }

        foreach (var effect in effects)
        {
            foreach (var target in targets)
            {
                //いらない？
                if (!CanEffect(target, targetUnit)) continue;

                //効果の範囲が単体の場合
                if (battleCommand.GetTargetType().targetRange != TargetRange.全体)
                {
                    //Debug.Log(battleCommand.GetCommand().animation);
                    Debug.Log(target.transform);
                    _BattleLogic.Instance.AnimationPlay(battleCommand.GetCommand().animation,target.transform);
                }

                //効果に情報を渡す
                //魔法or物理
                //反射(カウンター)・無効
                //魔法ダメージ効果・物理ダメージ効果
                effect.SetUp(new EffectInfo(EffectInfo.CommandType.スキル) { owner = owner, target = target });
                //ターゲットが存在しないとき新しいターゲットに変更する
                //target = newTarget;

                //効果の処理
                effect.Use(owner, target);
            }

            //演出を再生する
            yield return StartCoroutine(_BattleLogic.Instance.Play());
        }
        yield break;
    }

    public void 単体処理()
    {

    }

    public bool CanEffect(BattleCharacter target, TargetUnit targetUnit)
    {
        if (!target.IsDead()) return true;
        if (target.IsDead() && targetUnit == TargetUnit.死亡者) return true;

        return false;
    }
}
