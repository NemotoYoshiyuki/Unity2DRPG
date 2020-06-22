using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuffEffect : Effect
{
    public StatusType statusType;
    public int count;
    public int value;

    public override void Use(BattleCharacter owner, BattleCharacter target)
    {

        BattleDirectorController.Instance.AddBuff(target, new Buff(statusType, count, value));

        if (value >= 0)
        {
            BattleDirectorController.Instance.Message($"{target.CharacterName}の{statusType.ToString()}が　あがった");
        }
        else
        {
            BattleDirectorController.Instance.Message($"{target.CharacterName}の{statusType.ToString()}が　さがった");
        }
    }
}
