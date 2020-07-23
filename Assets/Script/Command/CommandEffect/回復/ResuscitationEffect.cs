using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResuscitationEffect : Effect
{
    public AnimationClip animationClip;
    public int rate;//蘇生する確率
    public int healRate;//蘇生した時のHp回復割合

    public override void Use(BattleCharacter owner, BattleCharacter target)
    {
        if (!target.IsDead())
        {
            BattleDirectorController.Instance.Message("しかし　なにもおきなかった！");
            return;
        }
        var healAmount = (float)target.status.maxHp * ((float)healRate / 100f);
        BattleDirectorController.Instance.Revival(target, (int)healAmount);
    }
}
