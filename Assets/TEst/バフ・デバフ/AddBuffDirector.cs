using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuffDirector : BattleDirector
{
    private BattleCharacter battleCharacter;
    private Buff buff;

    public AddBuffDirector(BattleCharacter battleCharacter, Buff buff)
    {
        this.battleCharacter = battleCharacter;
        this.buff = buff;
    }

    public override IEnumerator Execute()
    {
        battleCharacter.AddBuff(buff);
        yield break;
    }
}
