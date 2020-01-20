using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseCommand : BattleCommand
{

    public DefenseCommand()
    {

    }

    public DefenseCommand(BattleCharacter owner, BattleCharacter target) : base(owner, target)
    {

    }

    public override TargetType GetTargetType()
    {
        return new TargetType(TargetUnit.自分, TargetRange.なし);
    }

    public override List<CommandEffect> GetEffect()
    {
        //効果はない
        return new List<CommandEffect>();
    }
}
