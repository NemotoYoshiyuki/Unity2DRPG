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

    public override IEnumerator Execution()
    {
        int spellMp = spellData.mp;
        string spellMessage = $"{owner.CharacterName}は　{spellData.skillName}をとなえた"; ;
        _BattleLogic.Instance.Message(spellMessage);
        Debug.Log("呪文");

        //yield return StartCoroutine(message.ShowAuto(spellMessage));

        if (owner.status.mp <= spellMp)
        {
            //yield return StartCoroutine(message.ShowAuto("しかし ＭＰが たりない！"));
            _BattleLogic.Instance.Message("しかし ＭＰが たりない！");
            yield break;
        }

        owner.GainMp(spellMp);
        //yield return StartCoroutine(effectExecutor.Execution(spellCommand));
        yield break;
    }

    public override List<CommandEffect> GetEffect()
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
