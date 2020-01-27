using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySystem
{
    public List<ItemData> itemDatas = new List<ItemData>();

    public void AddItem(ItemData itemData)
    {
        itemDatas.Add(itemData);
    }

    public void UseItem(ItemData itemData)
    {
        itemDatas.Remove(itemData);
    }
}
