using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class TreasureBox : Interactable
{
    [Header("宝箱の識別番号")]
    [SerializeField] private ulong id;

    public Sprite enptyImage;//宝箱を開けた時置き換えられる画像

    [Header("宝箱の中身")]
    public int money = 0;
    public Item item = null;
    public Equipment equipment = null;

    private bool isOpen;
    private string sceneName => SceneController.Instance.CurrentScene;
    private string key => $"{sceneName}_宝箱を開けた_{id}";

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        id = UnityEditor.GlobalObjectId.GetGlobalObjectIdSlow(gameObject).targetObjectId;

        if (!VariablePersister.Exist(key)) return;
        isOpen = VariablePersister.GetBool(key);

        if (!isOpen)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = enptyImage;
        }
    }

    public override void OnInteractable()
    {
        GameEvent.Execute(OpenBox());
        //StartCoroutine(OpenBox());
    }


    public IEnumerator OpenBox()
    {
        MessageWindow messageWindow = MessageSystem.GetWindow();

        PlayerInteract.InteractableStart();

        if (isOpen)
        {
            yield return StartCoroutine(messageWindow.ShowClick("からっぽだ"));
            messageWindow.Close();
            PlayerInteract.InteractableEnd();
            yield break;
        }

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
        VariablePersister.SetBool(key, true);

        PlayerInteract.InteractableEnd();
        yield break;
    }

    public IEnumerator GetMony()
    {
        GameController.Money += money;
        yield return PicUp(money.ToString());
        yield break;
    }

    public IEnumerator GetItem()
    {
        InventorySystem.AddItem(item);
        yield return PicUp(item.itemName);
        yield break;
    }

    public IEnumerator GetEqip()
    {
        InventorySystem.AddEqip(equipment);
        yield return PicUp(equipment.name);
        yield break;
    }

    public IEnumerator PicUp(string itemName)
    {
        if (isOpen) yield break;
        isOpen = true;
        MessageWindow messageWindow = MessageSystem.GetWindow();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enptyImage;
        string ms = itemName + "を手に入れた";
        yield return StartCoroutine(MessageSystem.GetWindow().ShowClick(ms));
        messageWindow.Close();
        yield break;
    }
}
