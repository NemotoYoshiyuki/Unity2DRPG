using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemWindow : BaseWindow, ICancel
{
    public ItemSlot itemSlotPrefab;
    public GameObject itemList;
    public TextMeshProUGUI ItemDescription;
    public FieldEffect fieldEffect;

    private List<ItemSlot> itemSlots = new List<ItemSlot>();
    private List<ItemData> itemSouce;
    private ItemData selectedItem;
    private ItemData hoverItem;

    private void OnEnable()
    {
        Initialized();
    }

    private void OnDisable()
    {
        ClearItems();
    }

    public void _Open()
    {
        Open();
        MenuWindow.AddHistory(this);
        MenuWindow.AddHistory(new Undo(()=> Close()));
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
        }
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
        MenuWindow.AddHistory(new Undo(() => Open()));
        selectedItem = item.item;
        fieldEffect.UseItem(selectedItem);
        gameObject.SetActive(false);
        
        MenuWindow.AddHistory(item);
    }

    void ICancel.Undo()
    {
        Close();
    }
}
