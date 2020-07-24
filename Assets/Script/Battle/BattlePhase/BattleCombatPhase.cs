using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class BattleCombatPhase : MonoBehaviour
{
    public BattleCommandManager commandManager;

    public IEnumerator Combat()
    {
        //敵のコマンドを登録
        commandManager.RegisterEnemyCommand();
        var commands = commandManager.battleCommands;
        //コマンド並び替え
        commandManager.Sort();

        BattleController.instance.OnTrunStart();

        foreach (var command in commands)
        {
            BattleCharacter owner = command.owner;

            if (BattleController.instance.Settle()) continue;
            if (owner.IsDead()) continue;

            owner.onActionBefore?.Invoke();

            //行動可能な場合
            if (!owner.canAction) continue;

            //コマンド処理
            yield return StartCoroutine(CommandExecution(command));


            //効果実行
            if (!command.CanEffec()) continue;
            yield return EffctExecution(command);
        }

        //ターンエンド処理
        BattleController.instance.OnTurnEnd();
        commandManager.Clea();

        yield break;
    }

    //コマンド実行
    private IEnumerator CommandExecution(BattleCommand battleCommand)
    {
        //行動不可能な場合
        if (battleCommand.owner.canAction == false) yield break;

        //コマンド実行
        yield return StartCoroutine(battleCommand.Execution());
        yield return BattleDirectorController.Instance.Play();
        yield break;
    }

    //効果の処理
    private IEnumerator EffctExecution(BattleCommand battleCommand)
    {
        BattleCharacter owner = battleCommand.owner;
        List<BattleCharacter> targets = battleCommand.target;
        TargetUnit targetUnit = battleCommand.GetTargetType().targetUnit;
        PlayableAsset playableAsset = battleCommand.GetCommand().animation;
        var effects = battleCommand.GetEffect();

        //効果の範囲が全体の場合
        if (battleCommand.GetTargetType().targetRange == TargetRange.全体)
            BattleDirectorController.Instance.AnimationPlay(playableAsset);

        foreach (var effect in effects)
        {
            //単体処理
            foreach (var target in targets)
            {
                //ターゲットが存在しないとき新しいターゲットに変更する
                BattleCharacter newTarget = target;
                if (TargetUnit.死亡者 != targetUnit && target.IsDead())
                {
                    TargetFilter targetFilter = new TargetFilter();
                    newTarget = targetFilter.Auto(battleCommand);
                }

                //効果の処理
                if (battleCommand.GetTargetType().targetRange == TargetRange.単体)
                    BattleDirectorController.Instance.AnimationPlay(playableAsset, newTarget.transform);
                effect.Use(owner, newTarget);
                //演出を再生する
                yield return (BattleDirectorController.Instance.Play());
            }
        }
        yield break;
    }
}
