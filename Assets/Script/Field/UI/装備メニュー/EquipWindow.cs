using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class EquipWindow : BaseWindow
{
    public EquipParameter equipParameter;
    public EquipSlot equipSlot;
    public CharacterData character;
    public CharacterWindow characterWindow;
    private MenuGuide menuGuide => MenuWindow.instance.menuGuide;
    private Action onCancel;

    public override void Open()
    {
        MenuWindow.instance.focusWindow = this;
        UserSelect();

    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    public override void Cancel()
    {
        onCancel?.Invoke();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        equipSlot.ShowEqipSlot(character);
        equipSlot.Select();
        equipParameter.ShowParameter(character);
    }

    public void UserSelect()
    {
        var party = Party.GetMember();
        menuGuide.Show("誰の装備を変更しますか？");

        characterWindow.Select(0);
        characterWindow.ClearLisner();
        characterWindow.AddLisner((int index) =>
        {
            character = party[index];
            Show();
        });

        onCancel = (() =>
        {
            MenuWindow.instance.focusWindow = MenuWindow.instance;
            MenuWindow.instance.sideMenu.sideButtons[3].Select();
            menuGuide.Hide();
        });
    }
}
