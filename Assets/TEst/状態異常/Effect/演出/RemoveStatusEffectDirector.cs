using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveStatusEffectDirector : BattleDirector
{
    public BattleCharacter battleCharacter;
    public StatusEffect statusEffect;

    public RemoveStatusEffectDirector(BattleCharacter battleCharacter, StatusEffect statusEffect)
    {
        this.battleCharacter = battleCharacter;
        this.statusEffect = statusEffect;
    }

    public override IEnumerator Do()
    {
        //状態異常を解消する
        battleCharacter.Treatment();
        return base.Do();
    }
}
