using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    public List<ItemData> itemDatas = new List<ItemData>();

    public void AddItem(ItemData itemData)
    {
        itemDatas.Add(itemData);
    }

    public void AddItem(int id)
    {
        ItemData itemData = GameController.Instance.itemMasterData.items.FirstOrDefault(x => x.id == id);
        itemDatas.Add(itemData);
    }

    public void UseItem(ItemData itemData)
    {
        itemDatas.Remove(itemData);
    }

    public void UseItem(int id)
    {
        ItemData itemData = GameController.Instance.itemMasterData.items.FirstOrDefault(x => x.id == id);
        itemDatas.Remove(itemData);
    }

    public bool HasItem(int id)
    {
        return GameController.Instance.itemMasterData.items.Any(x => x.id == id);
    }
}
