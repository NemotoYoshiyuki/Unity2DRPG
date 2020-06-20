using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//バトルコマンドの効果を処理するクラスです
public class CommandEffectExecutor : MonoBehaviour
{

    public IEnumerator Execution(BattleCommand battleCommand)
    {
        BattleCharacter owner = battleCommand.owner;
        List<BattleCharacter> targets = battleCommand.target;
        TargetUnit targetUnit = battleCommand.GetTargetType().targetUnit;
        PlayableAsset playableAsset = battleCommand.GetCommand().animation;
        var effects = battleCommand.GetEffect();

        //効果の範囲が全体の場合
        if (battleCommand.GetTargetType().targetRange == TargetRange.全体)
        {
            BattleDirectorController.Instance.AnimationPlay(playableAsset);
        }

        foreach (var effect in effects)
        {
            foreach (var target in targets)
            {

                //効果の範囲が単体の場合
                if (battleCommand.GetTargetType().targetRange != TargetRange.全体)
                {
                    BattleDirectorController.Instance.AnimationPlay(playableAsset, target.transform);
                }

                //ターゲットが存在しないとき新しいターゲットに変更する
                if (target.IsDead())
                {
                    BattleCharacter newTarget = target;
                    //死亡キャラが選択肢の場合
                    if (TargetUnit.死亡者 != targetUnit)
                    {
                        TargetFilter targetFilter = new TargetFilter();
                        newTarget = targetFilter.Auto(targets);
                    }

                    //効果の処理
                    effect.Use(owner, newTarget);
                    //演出を再生する
                    yield return (BattleDirectorController.Instance.Play());
                }
            }
        }
        yield break;
    }
}
