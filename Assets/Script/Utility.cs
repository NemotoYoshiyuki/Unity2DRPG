using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    //--------------------------------------------------------------------------------
    // Listから要素をランダムで1つ取得する
    //--------------------------------------------------------------------------------
    public static T GetRandom<T>(List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
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

    public static void DebugLog(string message = "",
        [System.Runtime.CompilerServices.CallerMemberName] string callerMemberName = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int callerLineNumber = 0)
    {
        Debug.Log(message + "\n" + "メソッド：" + callerMemberName + "行:" + callerLineNumber);
    }
}
