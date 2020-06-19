using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveStatusEffect : CommandEffect
{
    public StatusEffectType effectType;

    public override void SetUp(EffectInfo effectInfo)
    {
        effectInfo.characteristic = EffectInfo.Characteristic.特殊;
        effectInfo.effectType = EffectInfo.EffectType.補助;
    }

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
