using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveStatusEffectDirector : BattleDirector
{
    private BattleCharacter battleCharacter;
    private StatusEffect statusEffect;

    public RemoveStatusEffectDirector(BattleCharacter battleCharacter, StatusEffect statusEffect)
    {
        this.battleCharacter = battleCharacter;
        this.statusEffect = statusEffect;
    }

    public override IEnumerator Execute()
    {
        //状態異常を解消する
        statusEffect.Refresh();
        battleCharacter.RemoveStatusEffect();
        yield return BattleMessage.Show(statusEffect.refreshMessage);
        yield break;
    }
}
