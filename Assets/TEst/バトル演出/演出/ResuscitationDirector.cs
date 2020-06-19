using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResuscitationDirector : BattleDirector
{
    public BattleCharacter character;
    public int healAmount;

    public ResuscitationDirector(BattleCharacter character, int healAmount)
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
