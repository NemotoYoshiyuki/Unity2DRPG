using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportEffect : CommandEffect
{
    public StatusType statusType;
    public int count;
    public int value;

    private EffectInfo effectInfo;

    public override void SetUp(EffectInfo effectInfo)
    {
        this.effectInfo = effectInfo;
        effectInfo.characteristic = EffectInfo.Characteristic.特殊;
        effectInfo.effectType = EffectInfo.EffectType.補助;
    }

    public override void Use(BattleCharacter owner, BattleCharacter target)
    {

        _BattleLogic.Instance.AddBuff(target, new Buff(statusType, count, value));

        if (value >= 0)
        {
            _BattleLogic.Instance.Message($"{target.CharacterName}の{statusType.ToString()}が　あがった");
        }
        else
        {
            _BattleLogic.Instance.Message($"{target.CharacterName}の{statusType.ToString()}が　さがった");
        }
    }
}
