using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveStatusEffect : Effect
{
    public StatusEffectType effectType;

    public override void Use(BattleCharacter owner, BattleCharacter target)
    {
        if (effectType == StatusEffectType.全て || target.GetStatusEffect().id == (int)effectType)
        {
            BattleDirectorController.Instance.RemoveStatusEffect(target);
        }
        else
        {
            //状態異常を解除できない、失敗した
            BattleDirectorController.Instance.Message("なにもおきなかった");
        }
    }
}
