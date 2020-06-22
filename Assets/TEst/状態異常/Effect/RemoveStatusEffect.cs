using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveStatusEffect : Effect
{
    public StatusEffectType effectType;

    public override void Use(BattleCharacter owner, BattleCharacter target)
    {
        //状態異常を解除できない、失敗した
        if (target.GetStatusEffect() == null)
        {
            BattleDirectorController.Instance.Message("なにもおきなかった");
            return;
        }
        else if (target.GetStatusEffect().id == (int)StatusEffectType.全て || target.GetStatusEffect().id == (int)effectType)
        {
            BattleDirectorController.Instance.RemoveStatusEffect(target);
        }
    }
}
