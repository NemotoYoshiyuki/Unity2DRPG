using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class BattleTargetInput : BattleCommandInput
{
    public Button selectButton;
    public GameObject buttonHolder;
    private List<Button> selectButtons= new List<Button>();

    public override void Close()
    {
        base.Close();
        ClearButtons();
    }

    public void Show(List<BattleCharacter> target)
    {
        CreateButto(target);
    }

    private void CreateButto(List<BattleCharacter> target)
    {
        foreach (var item in target)
        {
            Button button = Instantiate(selectButton, transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = item.CharacterName;
            button.onClick.AddListener(() => OnClick(item));
            button.transform.SetParent(buttonHolder.transform);
            selectButtons.Add(button);
        }
    }

    private void ClearButtons()
    {
        foreach (var button in selectButtons)
        {
            Destroy(button.gameObject);
        }
        selectButtons.Clear();
    }

    //ターゲット決定ボタン
    public void OnClick(BattleCharacter battleCharacter)
    {
        BattleInputController.instance.SelectTarget(battleCharacter);
    }
}
