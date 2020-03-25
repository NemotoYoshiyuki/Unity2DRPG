﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyWindow : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        int money = GameController.Instance.money;
        text.SetText(money + " Gold");
    }
}
