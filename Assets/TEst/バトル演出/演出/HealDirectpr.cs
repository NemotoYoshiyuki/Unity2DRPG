using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDirectpr : BattleDirector
{
    public BattleCharacter character;
    public int healAmount;

    public HealDirectpr(BattleCharacter character, int healAmount)
    {
        this.character = character;
        this.healAmount = healAmount;
    }

    public override IEnumerator Do()
    {
        character.Recover(healAmount);
        yield break;
    }
}
