using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportEffectDirector : BattleDirector
{
    public BattleCharacter battleCharacter;
    public Buff buff;

    public SupportEffectDirector(BattleCharacter battleCharacter, Buff buff)
    {
        this.battleCharacter = battleCharacter;
        this.buff = buff;
    }

    public override IEnumerator Do()
    {
        battleCharacter.AddBuff(buff);
        return base.Do();
    }
}
