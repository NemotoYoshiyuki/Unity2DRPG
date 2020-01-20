using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFormula
{
    public static int PhysicalAttackDamage(Status owner, Status target)
    {
        int damage = (owner.attack / 2) - (target.deffence / 4);
        if (damage <= 0) damage = 1;
        return damage;
    }
}
