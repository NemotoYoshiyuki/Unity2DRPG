using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand : BattleCommand
{
    public SkillData skillData;

    //コンストラクタ
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

    public override IEnumerator Execution()
    {
        int skillMp = skillData.mp;

        if (owner.status.mp <= skillMp)
        {
            //yield return StartCoroutine(message.ShowAuto("しかし ＭＰが たりない！"));
            _BattleLogic.Instance.Message("しかし ＭＰが たりない！");
            yield break;
        }

        owner.GainMp(skillMp);

        //効果を実行する
        yield return CommandEffectExecutor.Instance.Execution(this);
        yield break;
    }

    public override List<CommandEffect> GetEffect()
    {
        return skillData.effects;
    }

    public override Command GetCommand()
    {
        return skillData;
    }

    public override TargetType GetTargetType()
    {
        return new TargetType(skillData.targetUnit,skillData.targetRange);
    }
}