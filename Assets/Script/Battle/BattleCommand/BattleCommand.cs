using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleCommand
{
    public BattleCharacter owner;
    public List<BattleCharacter> target;

    internal bool canEffect = true;

    public BattleCommand()
    {

    }

    public BattleCommand(BattleCharacter owner, List<BattleCharacter> target)
    {
        this.owner = owner;
        this.target = target;
    }

    //ターゲットが単数の時
    public BattleCommand(BattleCharacter owner, BattleCharacter target)
    {
        this.owner = owner;
        this.target.Add(target);
    }

    public bool CanEffec() { return canEffect; }

    public virtual IEnumerator Execution() { yield break; }

    public virtual List<CommandEffect> GetEffect() { return null; }
    public virtual Command GetCommand() { return null; }

    public virtual TargetType GetTargetType() { return null; }
}

public class TargetType
{
    public TargetUnit targetUnit;
    public TargetRange targetRange;

    public TargetType(TargetUnit unit, TargetRange range)
    {
        this.targetUnit = unit;
        this.targetRange = range;
    }
}
