using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : BattleCommand
{
    private List<Effect> effects = new List<Effect>();
    public Skill skillData;
    public AttackCommand()
    {

    }

    public AttackCommand(BattleCharacter owner, BattleCharacter target) : base(owner, target)
    {

    }

    public override IEnumerator Execution()
    {
        //スキルデータ作成
        skillData = (Skill)ScriptableObject.CreateInstance(typeof(Skill));
        skillData.animation = BattleDirectorController.Instance.slash;
        BattleDirectorController.Instance.Message(owner.CharacterName + "の　攻撃");
        //武器によって再生するアニメーションを変更する
        //BattleDirectorController.Instance.NormalAttack(target[0]);
        //effects.Add(new PhysicalAttackEffect());
        yield break;
    }

    public override List<Effect> GetEffect()
    {
        PhysicalAttackEffect physicalAttack = new PhysicalAttackEffect();
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
