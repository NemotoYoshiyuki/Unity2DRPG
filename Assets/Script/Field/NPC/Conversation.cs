using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : Interactable
{
    public List<string> conversation;

    public override void OnInteractable()
    {
        StartCoroutine(Talk());
    }

    public IEnumerator Talk()
    {
        MessageWindow messageWindow = MessageSystem.GetWindow();

        PlayerInteract.InteractableStart();

        //会話文章の表示
        //display talk
        List<string> content = conversation;
        foreach (var item in content)
        {
            yield return StartCoroutine(messageWindow.ShowClick(item));
        }
        messageWindow.Close();

        PlayerInteract.InteractableEnd();
    }
}
