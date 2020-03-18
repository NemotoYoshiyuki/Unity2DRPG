using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

[MoonSharpUserData]
public class LuaEventScript : MonoBehaviour
{
    //メッセージイベント
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

    public void Choice(string c1, string c2)
    {
        StartCoroutine(Choise());
        IEnumerator Choise()
        {
            ChoiceWindow choice = MessageSystem.GetChoise();
            yield return choice.Choice(c1, c2);
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

    public void Wait(float time)
    {
        StartCoroutine(Wait());
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(time);
            LuaScript.instance.Resume();
            yield break;
        }
    }

    //BGMイベント
    //パーティーイベント
    public void PartyIn(int id)
    {
        //PlayerParty.instance.Join(id);
    }

    public PlayerCharacter GetCharacter(int id)
    {
        return PlayerParty.instance.GetMember(id);
    }

    //フラグ操作
    public bool GetFlag(string flagName)
    {
        return GameController.instance.flagManager.GetValue(flagName);
    }

    public void SetFlag(string flagName, bool value)
    {
        GameController.instance.flagManager.SetFlag(flagName, value);
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
