using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

public class test : MonoBehaviour
{
    void Start()
    {
        int i = 11;
        Debug.Log((i.ToString()));
        bool b = true;
        Debug.Log(b.ToString());

        string ii = "11";
        int ci = Convert.ToInt32(ii);
        Debug.Log(ci);
        bool cb = Convert.ToBoolean("true");
        Debug.Log(cb);
    }
}
