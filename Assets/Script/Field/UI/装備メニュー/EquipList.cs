using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class EquipList : BaseWindow
{
    public GameObject list;
    public EquipSlot equipSlot;
    public EquipParameter equipParameter;
    public SelectableButton selectableButton;
    public TextMeshProUGUI description;
    private List<SelectableButton> selectableButtons = new List<SelectableButton>();
    public void ShowWeaponList(CharacterData character)
    {
        //クリック　武器を装備　パラメータを更新　スロットを更新　リストアイテムを更新（E）
        //ホバー　　説明を表示　パラメータを変更
        //外す　　　武器を外す　パラメータを更新　スロットを更新　リストアイテムを更新（E）
        MenuWindow.instance.focusWindow = this;
        MenuWindow.instance.menuGuide.Show("どの武器を装備しますか");
        ClearEquipList();
        List<Weapon> weapons = InventorySystem.GetEquipments().Where(x => x is Weapon).Select(x => x as Weapon).ToList();
        if (weapons.Count == 0) return;
        foreach (var weapon in weapons)
        {
            SelectableButton _selectableButton = Instantiate(selectableButton, list.transform);
            TextMeshProUGUI buttonText = _selectableButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = weapon.name;
            _selectableButton.onClick.AddListener(() =>
            {
                character.equip.weapon = weapon;
                equipSlot.ShowEqipSlot(character);
                equipParameter.ShowParameter(character);
                buttonText.text = "E " + weapon.name;

            });

            _selectableButton.onHover = (() =>
            {
                equipParameter.ShowParameter(character, weapon);
                description.text = weapon.description;
            });
            //_selectableButton.transform.SetParent(list.transform);
            selectableButtons.Add(_selectableButton);
        }
        selectableButtons[0].Select();
    }

    public void ShowArmorList(CharacterData character)
    {
        MenuWindow.instance.focusWindow = this;
        MenuWindow.instance.menuGuide.Show("どの防具を装備しますか");
        ClearEquipList();
        List<Armor> armors = InventorySystem.GetEquipments().Where(x => x is Armor).Select(x => x as Armor).ToList();
        if (armors.Count == 0) return;
        foreach (var armor in armors)
        {
            SelectableButton _selectableButton = Instantiate(selectableButton, list.transform);
            TextMeshProUGUI buttonText = _selectableButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = armor.name;
            _selectableButton.onClick.AddListener(() =>
            {
                character.equip.armor = armor;
                equipSlot.ShowEqipSlot(character);
                equipParameter.ShowParameter(character);
                buttonText.text = "E " + armor.name;

            });

            _selectableButton.onHover = (() =>
            {
                equipParameter.ShowParameter(character, armor);
                description.text = armor.description;
            });
            //_selectableButton.transform.SetParent(list.transform);
            selectableButtons.Add(_selectableButton);
        }
        selectableButtons[0].Select();
    }

    public void ShowAccessoryList(CharacterData character)
    {
        MenuWindow.instance.focusWindow = this;
        MenuWindow.instance.menuGuide.Show("どの装飾を装備しますか");
        ClearEquipList();
        List<Accessory> accessorys = InventorySystem.GetEquipments().Where(x => x is Accessory).Select(x => x as Accessory).ToList();
        if (accessorys.Count == 0) return;
        foreach (var accessory in accessorys)
        {
            SelectableButton _selectableButton = Instantiate(selectableButton, list.transform);
            TextMeshProUGUI buttonText = _selectableButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = accessory.name;
            _selectableButton.onClick.AddListener(() =>
            {
                character.equip.accessory = accessory;
                equipSlot.ShowEqipSlot(character);
                equipParameter.ShowParameter(character);
                buttonText.text = "E " + accessory.name;

            });

            _selectableButton.onHover = (() =>
            {
                equipParameter.ShowParameter(character, accessory);
                description.text = accessory.description;
            });
            _selectableButton.transform.SetParent(list.transform);
            selectableButtons.Add(_selectableButton);
        }
        selectableButtons[0].Select();
    }

    public void ClearEquipList()
    {
        foreach (var item in selectableButtons)
        {
            Destroy(item.gameObject);
        }
        selectableButtons.Clear();
    }

    public override void Cancel()
    {
        ClearEquipList();
        equipSlot.Select();
    }
}
