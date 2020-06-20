using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealEffect : CommandEffect
{
    public AnimationClip animationClip;
    public int healAmount;

    public override void Use(BattleCharacter owner, BattleCharacter target)
    {
        BattleDirectorController.Instance.Heal(target, healAmount);
    }
}
