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

    public void Choice(string c1,string c2)
    {
        StartCoroutine(Choise());
        IEnumerator Choise()
        {
            ChoiceWindow choice = MessageSystem.GetChoise();
            yield return choice.Choice(c1,c2);
            LuaScript.instance.Resume();
            yield break;
        }
    }

    public int ChoiceResult()
    {
        ChoiceWindow choice = MessageSystem.GetChoise();
        return choice.Result();
    }

    public void End()
    {
        MessageWindow messageWindow = MessageSystem.instance.messageWindow;
        messageWindow.Close();
    }

    public PlayerCharacter GetCharacter(int id)
    {
        return PlayerParty.instance.GetMember(id);
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
