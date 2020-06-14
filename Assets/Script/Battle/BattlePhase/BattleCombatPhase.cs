using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCombatPhase : MonoBehaviour
{
    public BattleCommandManager commandManager;
    public CommandExecutor commandExecution;

    public IEnumerator Combat()
    {
        //敵のコマンドを登録
        commandManager.RegisterEnemyCommand();
        var commands = commandManager.battleCommands;
        //コマンド並び替え
        commandManager.Sort();

        foreach (var command in commands)
        {
            BattleCharacter owner = command.owner;
            if (owner.IsDead()) continue;
            //バトルの決着がついている時それ以上コマンドを実行しない
            if (BattleController.instance.Settle()) continue;

            //コマンド処理
            yield return StartCoroutine(commandExecution.Execution(command));
        }

        //ターンエンド処理
        BattleController.instance.OnTurnEnd();

        //バフ・デバフの更新
        foreach (var item in BattleController.instance.AlivePlayerCharacters)
        {
            Debug.Log("バフの更新");
            //item.battleStaus.StatusUpdate();
            item.battleStaus.BuffUpdate();
            item.battleStaus.StatusUpdate();
        }

        foreach (var item in BattleController.instance.AliveEnemyCharacters)
        {
            item.battleStaus.BuffUpdate();
            item.battleStaus.StatusUpdate();
        }

        commandManager.Clea();
        yield break;
    }
}
