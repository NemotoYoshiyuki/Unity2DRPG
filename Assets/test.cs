using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

public class test : MonoBehaviour
{
    public int rate;
    void Start()
    {
        int c = 0;
        for (int i = 0; i < 10000; i++)
        {
            if (BattleFormula.CheckRate(rate))
            {
                c++;
            }
        }
        Debug.Log(c);
    }
}
