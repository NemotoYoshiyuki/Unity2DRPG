using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCommand : BattleCommand
{
    public Spell spellData;

    public SpellCommand(Spell spellData)
    {
        this.spellData = spellData;
    }

    public SpellCommand(Spell spellData, BattleCharacter owner, List<BattleCharacter> target) : base(owner, target)
    {
        this.spellData = spellData;
    }

    public override IEnumerator Execution()
    {
        int spellMp = spellData.mp;
        string spellMessage = $"{owner.CharacterName}は　{spellData.skillName}をとなえた"; ;
        BattleDirectorController.Instance.Message(spellMessage);


        if (owner.status.mp <= spellMp)
        {
            canEffect = false;
            BattleDirectorController.Instance.Message("しかし ＭＰが たりない！");
            yield break;
        }

        //封印
        if (owner.CanCastSpell)
        {
            canEffect = false;
            BattleDirectorController.Instance.Message("しかし 呪文は 封印されいる");
            yield break;
        }
        owner.GainMp(spellMp);

        yield break;
    }

    public override List<Effect> GetEffect()
    {
        return spellData.effects;
    }

    public override Command GetCommand()
    {
        return spellData;
    }

    public override TargetType GetTargetType()
    {
        TargetUnit targetUnit = spellData.targetUnit;
        TargetRange targetRange = spellData.targetRange;
        return new TargetType(targetUnit, targetRange);
    }
}
