using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : BattleCommand
{
    private List<CommandEffect> effects = new List<CommandEffect>();
    public SkillData skillData;
    public AttackCommand()
    {

    }

    public AttackCommand(BattleCharacter owner, BattleCharacter target) : base(owner, target)
    {

    }

   public override IEnumerator Execution(){
       
       //
       //スキルデータ作成
       skillData = (SkillData)ScriptableObject.CreateInstance(typeof(SkillData));
       //武器によって再生するアニメーションを変更する
       _BattleLogic.Instance.NormalAttack();
       effects.Add(new PhysicalAttackEffect());
       //効果を実行する
        yield return CommandEffectExecutor.Instance.Execution(this);
        yield break;
   }

    public override List<CommandEffect> GetEffect()
    {
        PhysicalAttackEffect physicalAttack = new PhysicalAttackEffect() { damageRate = 1, critical = false };
        effects.Add(physicalAttack);
        return effects;
    }

    public override Command GetCommand()
    {
        return skillData;
    }

    public override TargetType GetTargetType()
    {
        return new TargetType(TargetUnit.相手, TargetRange.単体);
    }
}
