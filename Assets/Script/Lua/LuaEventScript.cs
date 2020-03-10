using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

[MoonSharpUserData]
public class LuaEventScript : MonoBehaviour
{

    public void Say(string txt)
    {
        StartCoroutine(Say());
        Debug.Log(txt);
        IEnumerator Say()
        {
            MessageWindow messageWindow = MessageSystem.instance.messageWindow;
            yield return StartCoroutine(messageWindow.ShowClick(txt));
            LuaScript.instance.Resume();
            yield break;
        }
    }

    public void Choice()
    {

    }

    public Player GetPlayer()
    {
        return null;
    }

    public InventorySystem GetInventory()
    {
        return null;
    }

    public PlayerParty GetParty()
    {
        return null;
    }
}
