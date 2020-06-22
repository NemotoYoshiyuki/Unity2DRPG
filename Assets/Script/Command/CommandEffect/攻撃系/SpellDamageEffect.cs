using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamageEffect : Effect
{
    public int damageAmount;

    public override void Use(BattleCharacter owner, BattleCharacter target)
    {
        BattleDirectorController.Instance.DamageLogic(target, damageAmount);
    }
}
