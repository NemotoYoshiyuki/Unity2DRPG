using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : Interactable
{
    public Shop shop;
    public List<Item> items;

    public override void OnInteractable()
    {
        GameEvent.Execute(Shopping());
    }

    public IEnumerator Shopping()
    {
        MessageWindow messageWindow = MessageSystem.GetWindow();
        yield return StartCoroutine(messageWindow.ShowClick("いらしゃいませ"));
        messageWindow.Close();

        var m_shop = Instantiate(shop);
        m_shop.product = items;
        m_shop.Open();
        yield return new WaitUntil(() => m_shop.isOpen == false);

        yield return StartCoroutine(messageWindow.ShowClick("またのおこしを"));
        messageWindow.Close();
        yield break;
    }
}
