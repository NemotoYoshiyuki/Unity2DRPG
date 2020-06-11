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

        foreach (var command in commands)
        {
            BattleCharacter owner = command.owner;
            if (owner.IsDead()) continue;
            //バトルの決着がついている時それ以上コマンドを実行しない
            if (BattleController.instance.Settle()) continue;
            yield return StartCoroutine(commandExecution.Execution(command));
        }

        //バフ・デバフの更新
        foreach (var item in BattleController.instance.AlivePlayerCharacters)
        {
            Debug.Log("バフの更新");
            item.battleStaus.UpdateBuff();
            item.battleStaus.Update();
        }

        foreach (var item in BattleController.instance.AliveEnemyCharacters)
        {
            item.battleStaus.UpdateBuff();
            item.battleStaus.Update();
        }

        commandManager.Clea();
        yield break;
    }
}
