using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamageEffect : CommandEffect
{
    public int damageAmount;

    private EffectInfo effectInfo;
    public override void SetUp(EffectInfo effectInfo)
    {
        this.effectInfo = effectInfo;
        effectInfo.characteristic = EffectInfo.Characteristic.魔法;
        effectInfo.effectType = EffectInfo.EffectType.攻撃;
    }

    public override void Use(BattleCharacter owner, BattleCharacter target)
    {
        BattleDirectorController.Instance.DamageLogic(effectInfo,damageAmount);
    }
}
