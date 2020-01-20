using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand : BattleCommand
{
    public SkillData skillData;

    public SkillCommand(SkillData skillData) : base()
    {
        this.skillData = skillData;
    }

    public SkillCommand(BattleCharacter owner, BattleCharacter target) : base(owner, target)
    {

    }

    public SkillCommand(SkillData skillData, BattleCharacter owner, List<BattleCharacter> target) : base(owner, target)
    {
        this.skillData = skillData;
    }

    public override List<CommandEffect> GetEffect()
    {
        return skillData.effects;
    }

    public override TargetType GetTargetType()
    {
        return new TargetType(skillData.targetUnit,skillData.targetRange);
    }
}