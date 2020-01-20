using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public MessageWindow messageWindow;
    public static MessageSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public static MessageWindow GetWindow()
    {
        return instance.messageWindow;
    }
}
