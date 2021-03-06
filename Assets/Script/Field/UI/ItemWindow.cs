﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemWindow : BaseWindow
{
    public ItemButton itemSlotPrefab;
    public GameObject itemList;
    public TextMeshProUGUI ItemDescription;
    public FieldEffect fieldEffect;

    private Action OnCancel;
    private List<ItemButton> itemSlots = new List<ItemButton>();
    private List<SelectableButton> selectables = new List<SelectableButton>();
    private ItemSlot selectedItem;

    public int selectedItemIndex
    {
        get { return Mathf.Clamp(selectedItemIndex, 0, itemSlots.Count); }
        private set { }
    }

    private void OnEnable()
    {
        MenuWindow.instance.sideMenu.Lock();
    }

    private void OnDisable()
    {
        MenuWindow.instance.sideMenu.Unlock();
    }

    public override void Open()
    {
        base.Open();
        MenuWindow.instance.focusWindow = this;
        Initialized();

        ////キャンセルが押されたら
        OnCancel = () =>
        {
            MenuWindow.instance.focusWindow = MenuWindow.instance;
            MenuWindow.instance.sideMenu.sideButtons[0].Select();
            Close();
        };
    }

    public override void Close()
    {
        CreateItemList();
        base.Close();
    }

    public override void Cancel()
    {
        OnCancel.Invoke();
    }

    private void Initialized()
    {
        List<Item> inventory = InventorySystem.GetItems();
        //前回の表示内容をクリア
        ClearItemList();
        if (inventory == null || inventory.Count <= 0) return;

        CreateItemList();
        itemSlots[0].selectable.Select();
        ShowItemDescription(itemSlots[0].item);
    }

    private void CreateItemList()
    {
        List<Item> items = new List<Item>(InventorySystem.GetItems());

        if (items == null || items.Count <= 0) return;

        //ボタン作成
        for (int i = 0; i < items.Count; i++)
        {
            Item item = items[i];
            ItemButton itemSlot = CreateButton(item);
            itemSlot.index = i;
            //itemSlot.transform.SetParent(itemList.transform);
            itemSlots.Add(itemSlot);
            selectables.Add(itemSlot.selectable);
        }
        RegisterNavigation();
    }

    public void RefreshItemList()
    {
        ClearItemList();
        List<Item> inventory = InventorySystem.GetItems();
        if (inventory == null || inventory.Count <= 0) return;

        CreateItemList();
        itemSlots[selectedItemIndex].selectable.Select();
        ShowItemDescription(itemSlots[selectedItemIndex].item);
    }

    private ItemButton CreateButton(Item itemData)
    {
        ItemButton itemSlot = Instantiate(itemSlotPrefab, itemList.gameObject.transform);
        itemSlot.SetUp(itemData);

        //クリック動作
        itemSlot.selectable.onClick.AddListener(() =>
        {
            selectedItemIndex = itemSlot.index;
            OnItemButtonClick(itemData);
        });

        itemSlot.selectable.onHover = (() => OnItemButtonHover(itemData));

        if (CanUse(itemData) == false) itemSlot.Invalid();
        return itemSlot;
    }

    public bool CanUse(Item item)
    {

        if (item.useType == UseType.マップ上 || item.useType == UseType.いつでも)
        {
            return true;
        }
        return false;
    }

    private void OnItemButtonClick(Item itemData)
    {
        ItemUse(itemData);
    }

    private void ItemUse(Item itemData)
    {
        fieldEffect.UseItem(itemData);

        //Close();

        OnCancel = () =>
        {
            Open();
            MenuWindow.instance.menuGuide.Hide();
        };
    }

    private void OnItemButtonHover(Item itemData)
    {
        ShowItemDescription(itemData);
    }

    private void ShowItemDescription(Item itemData)
    {
        ItemDescription.SetText(itemData.description);
    }

    private void CleraItemDescription()
    {
        ItemDescription.SetText(string.Empty);
    }

    private void ClearItemList()
    {
        itemSlots.ForEach(x => Destroy(x.gameObject));
        itemSlots.Clear();
        selectables.Clear();
        ItemDescription.SetText(string.Empty);
    }

    private void RegisterNavigation()
    {
        StartCoroutine(CreateNavigation());
    }

    //Button.Navigation.modeのExplicitに適切なButtonを割り当てます
    private IEnumerator CreateNavigation()
    {
        //Layoutgroupを使用しているため、レイアウトの構築が完了するまで待つ必要があります
        yield return new WaitForEndOfFrame();

        foreach (var item in selectables)
        {
            item.FindSelectable(selectables);
        }
        yield break;
    }
}