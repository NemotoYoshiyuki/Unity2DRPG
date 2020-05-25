using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class TreasureBox : Interactable
{
    public ItemData item;
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

        isObtain = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enptyImage;
        GameController.GetInventorySystem().AddItem(item);
        string ms = item.itemName + "を手に入れた";
        yield return StartCoroutine(MessageSystem.GetWindow().ShowClick(ms));
        messageWindow.Close();

        //フラグの保存
        GameController.Instance.mapFlag.SetFlag(FlagName,true);

        PlayerInteract.InteractableEnd();
        yield break;
    }

    public string FlagName => SceneController.Instance.CurrentScene + gameObject.name;
}
