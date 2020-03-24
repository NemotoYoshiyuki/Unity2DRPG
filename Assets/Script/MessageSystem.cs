using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public MessageWindow messageWindow;
    public ChoiceWindow choiceWindow;
    public static MessageSystem instance;
    public static MessageSystem Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<MessageSystem>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    protected static void Create()
    {
        MessageSystem controllerPrefab = Resources.Load<MessageSystem>("MessageSystem");
        instance = Instantiate(controllerPrefab);
    }

    public static MessageWindow GetWindow()
    {
        return Instance.messageWindow;
    }

    public static ChoiceWindow GetChoise()
    {
        return Instance.choiceWindow;
    }
}
