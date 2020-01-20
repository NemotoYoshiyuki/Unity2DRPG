using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCommand : BattleCommand
{
    public SpellData spellData;

    public SpellCommand(SpellData spellData)
    {
        this.spellData = spellData;
    }

    public SpellCommand(SpellData spellData, BattleCharacter owner, List<BattleCharacter> target) : base(owner, target)
    {
        this.spellData = spellData;
    }

    public override List<CommandEffect> GetEffect()
    {
        return spellData.effects;
    }

    public override TargetType GetTargetType()
    {
        TargetUnit targetUnit = spellData.targetUnit;
        TargetRange targetRange = spellData.targetRange;
        return new TargetType(targetUnit, targetRange);
    }
}
