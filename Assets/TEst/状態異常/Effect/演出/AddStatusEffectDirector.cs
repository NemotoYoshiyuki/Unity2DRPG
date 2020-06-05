using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStatusEffectDirector : BattleDirector
{
    public BattleCharacter battleCharacter;
    public StatusEffect statusEffect;

    public AddStatusEffectDirector(BattleCharacter battleCharacter, StatusEffect statusEffect)
    {
        this.battleCharacter = battleCharacter;
        this.statusEffect = statusEffect;
    }

    public override IEnumerator Do()
    {
        //状態異常を付与する
        battleCharacter.AddStatusEffect(statusEffect);
        statusEffect.OnAdd();
        return base.Do();
    }
}
