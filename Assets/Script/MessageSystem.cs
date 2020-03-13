using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public MessageWindow messageWindow;
    public ChoiceWindow choiceWindow;
    public static MessageSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public static MessageWindow GetWindow()
    {
        return instance.messageWindow;
    }

    public static ChoiceWindow GetChoise()
    {
        return instance.choiceWindow;
    }
}
