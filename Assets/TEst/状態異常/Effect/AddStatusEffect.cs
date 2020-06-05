using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStatusEffect : CommandEffect
{

    public StatusEffectType effectType;//なんの状態異常
    public int counter;//なんターン継続する

    public override void SetUp(EffectInfo effectInfo)
    {
        base.SetUp(effectInfo);
    }

    public override void Use(BattleCharacter owner, BattleCharacter target)
    {
        //状態異常を付与できない、効かない
        if (target.statusEffect != null)
        {
            _BattleLogic.Instance.Message("なにもおきなかった");
            return;
        }

        StatusEffect statusEffect = null;
        switch (effectType)
        {
            case StatusEffectType.どく:
                statusEffect = new Poison(counter,target);
                break;
            case StatusEffectType.まひ:
                statusEffect = new Palsy(counter, target);
                break;
            case StatusEffectType.すいみん:
                statusEffect = new Sleep(counter, target);
                break;
            case StatusEffectType.全て:
                break;
            default:
                break;
        }

        _BattleLogic.Instance.AddStatusEffect(target, statusEffect);
    }
}
