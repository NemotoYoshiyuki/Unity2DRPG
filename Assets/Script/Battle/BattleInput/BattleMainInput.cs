using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMainInput : BattleCommandInput
{
    public Button EscapeButton;

    void Start()
    {
        Debug.Log(BattleController.instance.CanEscape());
        if (!BattleController.instance.CanEscape())
        {
            EscapeButton.interactable = false;
        }
    }

    public void Fight()
    {
        BattleInputController.instance.Change<BattleFightInput>();
    }

    public void Escape()
    {
        BattleController.instance.OnEscape();
        BattleInputController.instance.OnInputFinish();
    }
}
