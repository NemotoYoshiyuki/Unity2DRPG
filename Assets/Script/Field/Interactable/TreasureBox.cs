using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class TreasureBox : Interactable
{
    public int money = 0;
    public Item item;
    public Equipment equipment;
    public Sprite enptyImage;//宝箱を開けた時置き換えられる画像
    bool isObtain;

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        isObtain = GameController.Instance.mapFlag.GetValue(FlagName);

        if (ItemExists())
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = enptyImage;
        }
    }

    public override void OnInteractable()
    {
        StartCoroutine(OpenBox());
    }

    private bool ItemExists()
    {
        return isObtain || item == null;
    }

    public IEnumerator OpenBox()
    {
        MessageWindow messageWindow = MessageSystem.GetWindow();

        PlayerInteract.InteractableStart();

        if (ItemExists())
        {
            yield return StartCoroutine(messageWindow.ShowClick("からっぽだ"));
            messageWindow.Close();
            PlayerInteract.InteractableEnd();
            yield break;
        }

        // isObtain = true;
        // SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // spriteRenderer.sprite = enptyImage;
        // GameController.GetInventorySystem().AddItem(item);
        // string ms = item.itemName + "を手に入れた";
        // yield return StartCoroutine(MessageSystem.GetWindow().ShowClick(ms));
        // messageWindow.Close();

        //お金が入っていた
        if (money < 0)
        {
            yield return GetMony();
        }

        //アイテムが入っていた
        if (item != null)
        {
            yield return GetItem();
        }

        //装備が入っていた
        if (equipment != null)
        {
            yield return GetEqip();
        }

        //フラグの保存
        GameController.Instance.mapFlag.SetFlag(FlagName, true);

        PlayerInteract.InteractableEnd();
        yield break;
    }

    public IEnumerator GetMony()
    {
        GameController.Instance.money += money;
        yield return PicUp(money.ToString());
        yield break;
    }

    public IEnumerator GetItem()
    {
        GameController.GetInventorySystem().AddItem(item);
        yield return PicUp(item.itemName);
        yield break;
    }

    public IEnumerator GetEqip()
    {
        GameController.GetInventorySystem().AddEqip(equipment);
        yield return PicUp(equipment.name);
        yield break;
    }

    public IEnumerator PicUp(string itemName)
    {
        if (isObtain) yield break;
        isObtain = true;
        MessageWindow messageWindow = MessageSystem.GetWindow();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enptyImage;
        string ms = itemName + "を手に入れた";
        yield return StartCoroutine(MessageSystem.GetWindow().ShowClick(ms));
        messageWindow.Close();
        yield break;
    }

    public string FlagName => SceneController.Instance.CurrentScene + gameObject.name;
}
