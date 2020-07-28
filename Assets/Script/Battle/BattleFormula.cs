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

    //--------------------------------------------------------------------------------
    // 指定した確率でtrueを返す
    //--------------------------------------------------------------------------------
    public static bool CheckRate(int rate)
    {
        if (UnityEngine.Random.Range(0, 100) < rate)
            return true;
        else
            return false;
    }

    public static bool CheckRate(float rate)
    {
        if ((UnityEngine.Random.value * 100.0f) < rate)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 分母/分子の割合でtrueを返します
    /// </summary>
    /// <param name="denominator">分母</param>
    /// <param name="numerator">分子</param>
    /// <returns></returns>
    public static bool CheckRate(int denominator, int numerator)
    {
        return UnityEngine.Random.Range(0, denominator) <= numerator;
    }
}
