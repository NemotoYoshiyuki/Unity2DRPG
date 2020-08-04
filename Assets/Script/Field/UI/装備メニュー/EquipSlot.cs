using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipSlot : BaseWindow
{
    public SelectableButton WeopnButton;
    public SelectableButton ArmerButton;
    public SelectableButton accessoryButton;
    public TextMeshProUGUI weopnText;
    public TextMeshProUGUI armerText;
    public TextMeshProUGUI accessoryText;
    public TextMeshProUGUI eqipDescription;

    public EquipWindow equipWindow;
    public EquipList equipList;
    public SelectableButton[] slotButton;
    private int selectIndex = 0;

    public void Select()
    {
        MenuWindow.instance.focusWindow = this;
        slotButton[selectIndex].Select();
        MenuWindow.instance.menuGuide.Show("どの装備を変更しますか？");
    }

    public void Refresh()
    {
        selectIndex = 0;
    }

    public void ShowEqipSlot(CharacterData character)
    {
        //現在装備している装備を表示する
        Equip equip = character.equip;
        weopnText.text = equip.weapon != null ? $"武器　{equip.weapon.name}" : "武器　ーーーーーー";
        armerText.text = equip.armor != null ? $"防具　{equip.armor.name}" : "防具　ーーーーーー";
        accessoryText.text = equip.accessory != null ? $"装飾　{equip.accessory.name}" : "装飾　ーーーーーー";

        //クリック動作
        WeopnButton.onClick.AddListener((() => { equipList.ShowWeaponList(character); }));
        ArmerButton.onClick.AddListener(() => { equipList.ShowArmorList(character); });
        accessoryButton.onClick.AddListener(() => { equipList.ShowAccessoryList(character); });

        //セレクトされたとき装備の情報を表示する
        WeopnButton.onHover = (() => { ShowDescription(equip.weapon); });
        ArmerButton.onHover = (() => { ShowDescription(equip.armor); });
        accessoryButton.onHover = (() => { ShowDescription(equip.accessory); });
    }

    public void ShowDescription(Equipment equipment)
    {
        if (equipment == null)
        {
            eqipDescription.text = string.Empty;
            return;
        }

        eqipDescription.text = equipment.description;
    }

    public override void Cancel()
    {
        MenuWindow.instance.focusWindow = equipWindow;
        equipWindow.Close();
        equipWindow.UserSelect();
    }
}
