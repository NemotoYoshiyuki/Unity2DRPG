using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand : BattleCommand
{
    public Skill skillData;

    public SkillCommand(Skill skillData) : base()
    {
        this.skillData = skillData;
    }

    public SkillCommand(BattleCharacter owner, BattleCharacter target) : base(owner, target)
    {

    }

    public SkillCommand(Skill skillData, BattleCharacter owner, List<BattleCharacter> target) : base(owner, target)
    {
        this.skillData = skillData;
    }

    public override IEnumerator Execution()
    {
        int skillMp = skillData.mp;

        if (owner.status.mp <= skillMp)
        {
            canEffect = false;
            BattleDirectorController.Instance.Message("しかし ＭＰが たりない！");
            yield break;
        }

        owner.GainMp(skillMp);

        yield break;
    }

    public override List<Effect> GetEffect()
    {
        return skillData.effects;
    }

    public override Command GetCommand()
    {
        return skillData;
    }

    public override TargetType GetTargetType()
    {
        return new TargetType(skillData.targetUnit, skillData.targetRange);
    }
}