using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStatusEffectDirector : BattleDirector
{
    private BattleCharacter battleCharacter;
    private StatusEffect statusEffect;

    public AddStatusEffectDirector(BattleCharacter battleCharacter, StatusEffect statusEffect)
    {
        this.battleCharacter = battleCharacter;
        this.statusEffect = statusEffect;
    }

    public override IEnumerator Execute()
    {

        if (battleCharacter.GetStatusEffect() != null)
        {
            battleCharacter.GetStatusEffect().Refresh();
        }

        //状態異常を付与する
        battleCharacter.AddStatusEffect(statusEffect);
        statusEffect.OnAdd();
        yield return BattleMessage.Show(statusEffect.addMessage);
        yield break;
    }
}
