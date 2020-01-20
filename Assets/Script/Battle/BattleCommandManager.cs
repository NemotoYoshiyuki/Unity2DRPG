using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleCommandManager : MonoBehaviour
{
    [SerializeReference] public List<BattleCommand> battleCommands = new List<BattleCommand>();
    private Queue<BattleCommand> _battleCommands;//行動可能なキャラのリスト

    public void Add(BattleCommand battleCommand)
    {
        battleCommands.Add(battleCommand);
    }

    public void Sort()
    {
        //素早さ順に並び替えます
    }

    public void Clea()
    {
        battleCommands.Clear();
    }

    public bool Complete()
    {
        //全てのCharacterのコマンドが登録された状態を示す
        return true;
    }

    public void Dequeue()
    {
        //Queue<T> の先頭にあるオブジェクトを削除し、返します。
    }

    public bool Finish()
    {
        //全てDequeueをした
        return true;
    }

    public void RegisterEnemyCommand()
    {
        List<EnemyCharacter> enemies = new List<EnemyCharacter>();
        enemies = BattleController.instance.enemyCharacters;
        foreach (var enemy in enemies)
        {
            BattleCommand enemyCommand = CreateEnemyCommand(enemy);
            battleCommands.Add(enemyCommand);
        }
    }

    //敵のコマンドを作成します
    //コマンドリストからランダムに一つ選びバトルコマンドに変換して登録します
    private BattleCommand CreateEnemyCommand(EnemyCharacter enemy)
    {
        List<Command> enemyCommands = enemy.GetCommands();
        Command randomChoiceCommand = enemyCommands.ElementAt(Random.Range(0, enemyCommands.Count));

        EnemyCharacter owner = enemy;
        TargetFilter targetFilter = new TargetFilter();
        List<BattleCharacter> target = null;

        switch (randomChoiceCommand)
        {
            case SpellData spellData:
                target = targetFilter.EnemyToFilter(owner, spellData.targetUnit, spellData.targetRange);
                return new SpellCommand(spellData, owner, target);
            case SkillData skillData:
                target = targetFilter.EnemyToFilter(owner, skillData.targetUnit, skillData.targetRange);
                return new SkillCommand(skillData, owner, target);
            default:
                break;
        }

        throw new System.Exception("Enemyの行動を作成できませんでした");
    }
}
