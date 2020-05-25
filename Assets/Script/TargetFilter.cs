using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetFilter
{
    public List<BattleCharacter> PlayerToGroup(BattleCommand battleCommand)
    {
        List<PlayerCharacter> playerCharacters = BattleController.instance.AlivePlayerCharacters;
        List<EnemyCharacter> enemyCharacters = BattleController.instance.AliveEnemyCharacters;
        BattleCharacter owner = battleCommand.owner;

        TargetType targetType = battleCommand.GetTargetType();
        TargetUnit targetUnit = targetType.targetUnit;
        TargetRange targetRange = targetType.targetRange;

        IReadOnlyCollection<BattleCharacter> target = new List<BattleCharacter>();

        switch (targetUnit)
        {
            case TargetUnit.自分:
                return new List<BattleCharacter>() { owner };
            case TargetUnit.味方:
                target = playerCharacters;
                break;
            case TargetUnit.相手:
                target = enemyCharacters;
                break;
            case TargetUnit.死亡者:
                target = playerCharacters.Where(x => x.IsDead()).ToList();
                break;
            default:
                break;
        }
        return target.Cast<BattleCharacter>().ToList();
    }

    public List<BattleCharacter> PlayerToFilter(BattleCommand battleCommand)
    {
        List<PlayerCharacter> playerCharacters = BattleController.instance.AlivePlayerCharacters;
        List<EnemyCharacter> enemyCharacters = BattleController.instance.AliveEnemyCharacters;
        BattleCharacter owner = battleCommand.owner;

        TargetType targetType = battleCommand.GetTargetType();
        TargetUnit targetUnit = targetType.targetUnit;
        TargetRange targetRange = targetType.targetRange;

        IReadOnlyCollection<BattleCharacter> target = new List<BattleCharacter>();

        switch (targetUnit)
        {
            case TargetUnit.自分:
                return new List<BattleCharacter>() { owner };
            case TargetUnit.味方:
                target = playerCharacters;
                break;
            case TargetUnit.相手:
                target = enemyCharacters;
                break;
            case TargetUnit.死亡者:
                target = playerCharacters.Where(x => x.IsDead()).ToList();
                break;
            default:
                break;
        }

        switch (targetRange)
        {
            case TargetRange.単体:
                return Randam(target);
            case TargetRange.全体:
                return target.Cast<BattleCharacter>().ToList();
            default:
                break;
        }
        return null;
    }

    public List<BattleCharacter> EnemyToFilter(BattleCommand battleCommand)
    {
        BattleCharacter owner = battleCommand.owner;
        TargetType targetType = battleCommand.GetTargetType();
        TargetUnit targetUnit = targetType.targetUnit;
        TargetRange targetRange = targetType.targetRange;
        return EnemyToFilter(owner, targetUnit, targetRange);
    }

    public List<BattleCharacter> EnemyToFilter(BattleCharacter owner, TargetUnit targetUnit, TargetRange targetRange)
    {
        List<PlayerCharacter> playerCharacters = BattleController.instance.AlivePlayerCharacters;
        List<EnemyCharacter> enemyCharacters = BattleController.instance.AliveEnemyCharacters;

        IReadOnlyCollection<BattleCharacter> target = new List<BattleCharacter>();
        switch (targetUnit)
        {
            case TargetUnit.自分:
                return new List<BattleCharacter>() { owner };
            case TargetUnit.味方:
                target = enemyCharacters;
                break;
            case TargetUnit.相手:
                target = playerCharacters;
                break;
            case TargetUnit.死亡者:
                target = enemyCharacters.Where(x => x.IsDead()).ToList();
                break;
            default:
                break;
        }

        switch (targetRange)
        {
            case TargetRange.単体:
                return Randam(target);
            case TargetRange.全体:
                return target.Cast<BattleCharacter>().ToList();
            default:
                break;
        }
        return null;
    }

    public List<BattleCharacter> Randam(IReadOnlyCollection<BattleCharacter> target)
    {
        var random = target.ElementAt(Random.Range(0, target.Count));
        return new List<BattleCharacter>() { random };
    }

    public List<BattleCharacter> GetP(BattleCommand battleCommand)
    {
        IReadOnlyCollection<PlayerCharacter> playerCharacters = BattleController.instance.AlivePlayerCharacters;
        IReadOnlyCollection<EnemyCharacter> enemyCharacters = BattleController.instance.AliveEnemyCharacters;
        return GetBattleCharacters(battleCommand, playerCharacters, enemyCharacters);
    }

    public List<BattleCharacter> GetE(BattleCommand battleCommand)
    {
        List<PlayerCharacter> playerCharacters = BattleController.instance.AlivePlayerCharacters;
        List<EnemyCharacter> enemyCharacters = BattleController.instance.AliveEnemyCharacters;
        return GetBattleCharacters(battleCommand, enemyCharacters, playerCharacters);
    }

    public List<BattleCharacter> GetBattleCharacters(BattleCommand battleCommand, IReadOnlyCollection<BattleCharacter> Friend, IReadOnlyCollection<BattleCharacter> Opponent)
    {
        BattleCharacter owner = battleCommand.owner;
        TargetType targetType = battleCommand.GetTargetType();
        TargetUnit targetUnit = targetType.targetUnit;
        TargetRange targetRange = targetType.targetRange;
        IReadOnlyCollection<BattleCharacter> target = new List<BattleCharacter>();
        switch (targetUnit)
        {
            case TargetUnit.自分:
                return new List<BattleCharacter>() { owner };
            case TargetUnit.味方:
                target = Friend;
                break;
            case TargetUnit.相手:
                target = Opponent;
                break;
            case TargetUnit.死亡者:
                target = Friend.Where(x => x.IsDead()).ToList();
                break;
            default:
                break;
        }

        switch (targetRange)
        {
            case TargetRange.単体:
                return Randam(target);
            case TargetRange.全体:
                return target.Cast<BattleCharacter>().ToList();
            default:
                break;
        }
        return null;
    }
}
