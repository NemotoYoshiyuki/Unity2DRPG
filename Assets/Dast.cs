using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Dast : MonoBehaviour
{
    public string data = "pen";
    [SerializeField]
    public string a => "This is a {data}.";
    public string v => $"This is a {data}.";

    [SerializeField]
    public string y { get; set; }

    private void Start()
    {
        string strText = $"This is a {data}.";
        Debug.Log(strText);

        string b = $"{a}";
        Debug.Log(b);
    }
}

