using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStatusEffect : CommandEffect
{

    public StatusEffectType effectType;//なんの状態異常
    public int counter;//なんターン継続する

    public override void Use(BattleCharacter owner, BattleCharacter target)
    {
        //状態異常を付与できない、効かない
        if (target.GetStatusEffect() != null)
        {
            BattleDirectorController.Instance.Message("なにもおきなかった");
            return;
        }

        StatusEffect statusEffect = null;
        switch (effectType)
        {
            case StatusEffectType.どく:
                statusEffect = new Poison(target, counter);
                break;
            case StatusEffectType.まひ:
                statusEffect = new Palsy(target, counter);
                break;
            case StatusEffectType.すいみん:
                statusEffect = new Sleep(target, counter);
                break;
            case StatusEffectType.全て:
                break;
            default:
                break;
        }

        BattleDirectorController.Instance.AddStatusEffect(target, statusEffect);
    }
}
