using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMessage : MonoBehaviour
{
    protected static BattleMessage instance;
    public MessageWindow messageWindow;

    private void Awake()
    {
        instance = this;
    }

    public static MessageWindow GetWindow()
    {
        return instance.messageWindow;
    }

    public static IEnumerator Show(string message)
    {
        yield return instance.messageWindow.ShowAuto(message);
        yield break;
    }
}
