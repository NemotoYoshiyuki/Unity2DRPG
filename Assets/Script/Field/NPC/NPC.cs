using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("NPC");
    }

    public override void OnInteractable()
    {
        StartCoroutine(Talk());
    }

    public IEnumerator Talk()
    {
        MessageWindow messageWindow = MessageSystem.GetWindow();
        Conversation conversation = GetComponent<Conversation>();

        InteractableStart();

        //会話文章の表示
        //display talk
        List<string> content = conversation.conversation;
        foreach (var item in content)
        {
            yield return StartCoroutine(messageWindow.ShowClick(item));
        }
        messageWindow.Close();

        InteractableEnd();
    }
}
