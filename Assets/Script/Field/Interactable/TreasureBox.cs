using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class TreasureBox : Interactable
{
    public ItemData item;
    public Sprite enptyImage;
    bool isObtain;

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
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

        InteractableStart();

        if (ItemExists())
        {
            yield return StartCoroutine(messageWindow.ShowClick("からっぽだ"));
            messageWindow.Close();
            InteractableEnd();
            yield break;
        }

        isObtain = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enptyImage;
        GameController.GetInventorySystem().AddItem(item);
        string ms = item.itemName + "を手に入れた";
        yield return StartCoroutine(MessageSystem.GetWindow().ShowClick(ms));
        messageWindow.Close();

        

        InteractableEnd();
        yield break;
    }
}
