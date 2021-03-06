﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public Button ContinueButton;
    public Button newGameButton;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (ContinueButton.gameObject.activeSelf == true)
        {
            ContinueButton.Select();
        }
        else
        {
            newGameButton.Select();
        }
    }
}
