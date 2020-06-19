using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResuscitationEffect : CommandEffect
{
    public AnimationClip animationClip;
    public int rate;//蘇生する確率
    public int healRate;//蘇生した時のHp回復割合

    private EffectInfo effectInfo;
    public override void SetUp(EffectInfo effectInfo)
    {
        this.effectInfo = effectInfo;
        effectInfo.characteristic = EffectInfo.Characteristic.特殊;
        effectInfo.effectType = EffectInfo.EffectType.回復;
    }

    public override void Use(BattleCharacter owner, BattleCharacter target)
    {
        int healAmount = target.status.maxHp / healRate;
        BattleDirectorController.Instance.Revival(effectInfo.target, healAmount);
    }
}
