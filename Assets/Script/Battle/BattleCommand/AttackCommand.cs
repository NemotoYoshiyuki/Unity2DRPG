using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : BattleCommand
{
    private List<CommandEffect> effects = new List<CommandEffect>();

    public AttackCommand()
    {

    }

    public AttackCommand(BattleCharacter owner, BattleCharacter target) : base(owner, target)
    {

    }

    public override List<CommandEffect> GetEffect()
    {
        PhysicalAttackEffect physicalAttack = new PhysicalAttackEffect() { damageRate = 1, critical = false };
        effects.Add(physicalAttack);
        return effects;
    }

    public override TargetType GetTargetType()
    {
        return new TargetType(TargetUnit.相手, TargetRange.単体);
    }
}
