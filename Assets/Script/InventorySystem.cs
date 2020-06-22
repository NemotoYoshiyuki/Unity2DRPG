using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    public List<Item> itemDatas = new List<Item>();
    public List<Equipment> equipments = new List<Equipment>();

    public void AddItem(Item itemData)
    {
        itemDatas.Add(itemData);
    }

    public void AddEqip(Equipment equipment){
        equipments.Add(equipment);
    }

    public void AddItem(int id)
    {
        Item itemData = GameController.Instance.itemMasterData.Get().FirstOrDefault(x => x.id == id);
        itemDatas.Add(itemData);
    }

    public void UseItem(Item itemData)
    {
        itemDatas.Remove(itemData);
    }

    public void UseItem(int id)
    {
        Item itemData = GameController.Instance.itemMasterData.Get().FirstOrDefault(x => x.id == id);
        itemDatas.Remove(itemData);
    }

    public bool HasItem(int id)
    {
        return itemDatas.Any(x => x.id == id);
    }
}
