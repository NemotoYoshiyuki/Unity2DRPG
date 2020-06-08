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
        if (target.statusEffect == null)
        {
            _BattleLogic.Instance.Message("なにもおきなかった");
            return;
        }
        else if (target.statusEffect.id == (int)StatusEffectType.全て || target.statusEffect.id == (int)effectType)
        {
            _BattleLogic.Instance.RemoveStatusEffect(target);
        }
    }
}
