using UnityEngine;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Sprite icon;
    public ItemData item;

    public int index;

    public TextMeshProUGUI text;


    public SelectableButton selectable;

    private void Start()
    {
        selectable = GetComponent<SelectableButton>();
    }

    public void SetText(string text)
    {
        this.text.SetText(text);
    }

    public void SetUp(ItemData itemData)
    {
        this.item = itemData;
        SetText(item.itemName);
    }

    public void Invalid()
    {
        //無効化
        //テキストとアイコンを薄く
        //ボタンを暗転
    }
}
