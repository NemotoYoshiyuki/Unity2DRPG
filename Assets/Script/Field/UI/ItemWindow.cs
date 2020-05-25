using System.Collections;
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
        List<ItemData> inventory = GameController.GetInventorySystem().itemDatas;
        if (inventory == null || inventory.Count < 0) return;

        ClearItemList();
        CreateItemList();
        itemSlots[0].selectable.Select();
        ShowItemDescription(itemSlots[0].item);
    }

    private void CreateItemList()
    {
        List<ItemData> items = new List<ItemData>(GameController.GetInventorySystem().itemDatas);
        if (items == null || items.Count < 0) return;

        //ボタン作成
        for (int i = 0; i < items.Count; i++)
        {
            ItemData item = items[i];
            ItemButton itemSlot = CreateButton(item);
            itemSlot.index = i;
            itemSlot.transform.SetParent(itemList.transform);
            itemSlots.Add(itemSlot);
            selectables.Add(itemSlot.selectable);
        }
        RegisterNavigation();
    }

    public void RefreshItemList()
    {
        ClearItemList();
        CreateItemList();

        itemSlots[selectedItemIndex].selectable.Select();
        ShowItemDescription(itemSlots[selectedItemIndex].item);
    }

    private ItemButton CreateButton(ItemData itemData)
    {
        ItemButton itemSlot = Instantiate(itemSlotPrefab);
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

    public bool CanUse(ItemData item)
    {
        if (item.useType != UseType.戦闘中)
        {
            return true;
        }
        return false;
    }

    private void OnItemButtonClick(ItemData itemData)
    {
        ItemUse(itemData);
    }

    private void ItemUse(ItemData itemData)
    {
        fieldEffect.UseItem(itemData);
        //Close();

        OnCancel = () =>
        {
            Open();
        };
    }

    private void OnItemButtonHover(ItemData itemData)
    {
        ShowItemDescription(itemData);
    }

    private void ShowItemDescription(ItemData itemData)
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