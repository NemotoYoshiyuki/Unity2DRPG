using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemWindow : BaseWindow
{
    public ItemSlot itemSlotPrefab;
    public GameObject itemList;
    public TextMeshProUGUI ItemDescription;
    public FieldEffect fieldEffect;

    private List<ItemSlot> itemSlots = new List<ItemSlot>();
    private List<ItemData> itemSouce;
    private ItemData selectedItem;
    private ItemData hoverItem;

    public Action OnCancel;
    private void OnDisable()
    {
        Close();
    }

    public override void Open()
    {
        base.Open();
        MenuWindow.instance.sideMenu.Lock();
        MenuWindow.instance.currentWindow = this;
        Initialized();

        ////キャンセルが押されたら
        OnCancel = () =>
        {
            MenuWindow.instance.currentWindow = MenuWindow.instance;
            MenuWindow.instance.sideMenu.Unlock();
            Close();
        };
    }

    public override void Close()
    {
        base.Close();
        ClearItems();
    }

    public void Initialized()
    {
        this.itemSouce = GameController.GetInventorySystem().itemDatas;
        this.selectedItem = itemSouce[0];

        for (int i = 0; i < itemSouce.Count; i++)
        {
            ItemSlot itemSlot = Instantiate(itemSlotPrefab);
            itemSlot.index = i;
            itemSlot.owner = this;
            itemSlot.item = itemSouce[i];

            itemSlot.SetText(itemSouce[i].itemName);
            itemSlot.transform.SetParent(itemList.transform);
            itemSlots.Add(itemSlot);

            if (CanUse(itemSlot.item) == true) itemSlot.selectable.interactable = false;
        }
    }

    private bool CanUse(ItemData item)
    {
        if (item.useType != UseType.戦闘中)
        {
            return true;
        }
        return false;   
    }

    public void ClearItems()
    {
        foreach (var item in itemSlots)
        {
            Destroy(item.gameObject);
        }
        itemSlots.Clear();
    }

    public void ObjectHoveredEnter(ItemSlot item)
    {
        hoverItem = itemSouce[item.index];
        //説明文の更新
        ItemDescription.SetText(hoverItem.description);
    }

    public void ObjectOnclic(ItemSlot item)
    {

        selectedItem = item.item;
        fieldEffect.UseItem(selectedItem);
        Close();

        OnCancel = () =>
        {
            Open();
        };
    }

    public override void Cancel()
    {
        OnCancel.Invoke();
    }
}
