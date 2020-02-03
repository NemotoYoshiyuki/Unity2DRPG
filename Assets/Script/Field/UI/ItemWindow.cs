using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemWindow : MonoBehaviour
{
   
    public MenuItem menuItem;
    public GameObject itemList;
    public TextMeshProUGUI ItemDescription;
    public FieldEffect fieldEffect;


    public List<ItemData> itemSouce;
    public ItemData selectedItem;
    public ItemData hoverItem;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Open()
    {

    }

    private void OnEnable()
    {
        Initialized();
    }

    public ItemSlot[] itemSlots;
    private void Initialized()
    {
        this.itemSouce = GameController.GetInventorySystem().itemDatas;
        this.selectedItem = itemSouce[0];

        for (int i = 0; i < itemSouce.Count; i++)
        {
            MenuItem _menuItem = Instantiate(menuItem);
            _menuItem.index = i;
            _menuItem.text.SetText(itemSouce[i].itemName);

            _menuItem.transform.parent = itemList.transform;

            _menuItem.onHover += (int index) =>
            {
                ObjectHoveredEnter(index);
            };

            _menuItem.onLeftClick += (int index) =>
            {
                selectedItem = itemSouce[index];
                fieldEffect.Item(selectedItem);
                gameObject.SetActive(false);
            };
        }
    }

    public void ObjectHoveredEnter(int index)
    {
        hoverItem = itemSouce[index];
        ItemDescription.SetText(hoverItem.description);
    }


    public void ObjectHoveredEnter(ItemSlot item)
    {
        hoverItem = itemSouce[item.index];
        //説明文の更新
        ItemDescription.SetText(hoverItem.description);
    }
}
