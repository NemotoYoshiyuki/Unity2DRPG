using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealEffect : CommandEffect
{
    public AnimationClip animationClip;
    public int healAmount;

    private EffectInfo effectInfo;
    public override void SetUp(EffectInfo effectInfo)
    {
        this.effectInfo = effectInfo;
        effectInfo.characteristic = EffectInfo.Characteristic.特殊;
        effectInfo.effectType = EffectInfo.EffectType.回復;
    }

    public override void Use(BattleCharacter owner, BattleCharacter target)
    {
        BattleDirectorController.Instance.Heal(effectInfo.target, healAmount);
    }
}
