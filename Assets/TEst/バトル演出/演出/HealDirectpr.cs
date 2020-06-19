using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDirectpr : BattleDirector
{
    private BattleCharacter character;
    private int healAmount;

    public HealDirectpr(BattleCharacter character, int healAmount)
    {
        this.character = character;
        this.healAmount = healAmount;
    }

    public override IEnumerator Execute()
    {
        character.Recover(healAmount);
        yield break;
    }
}
