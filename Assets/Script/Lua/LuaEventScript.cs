using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

[MoonSharpUserData]
public class LuaEventScript : MonoBehaviour
{

    public void Print(object obj)
    {
        Debug.Log(obj);
    }

    //メッセージイベント
    public void Say(string txt)
    {
        StartCoroutine(Say());
        Debug.Log(txt);
        IEnumerator Say()
        {
            MessageWindow messageWindow = MessageSystem.Instance.messageWindow;
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
        MessageWindow messageWindow = MessageSystem.Instance.messageWindow;
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

    public void FadeIn()
    {
        StartCoroutine(Fade());
        IEnumerator Fade()
        {
            yield return StartCoroutine(SceneFader.FadeSceneIn());
            LuaScript.instance.Resume();
            yield break;
        }
    }

    public void FadeOut()
    {
        StartCoroutine(Fade());
        IEnumerator Fade()
        {
            yield return StartCoroutine(SceneFader.FadeSceneOut());
            LuaScript.instance.Resume();
            yield break;
        }
    }

    //BGMイベント
    //パーティーイベント
    public void PartyIn(int id)
    {
        Party.Instance.Join(id);
    }

    public int GetMoney()
    {
        return GameController.Money;
    }

    public void GainMoney(int mony)
    {
        GameController.Money -= mony;
    }

    public void AddMony(int mony)
    {
        GameController.Money += mony;
    }

    //アイテム操作
    public void AddItem(int id)
    {
        InventorySystem.AddItem(id);
    }

    public void GainItem(int id)
    {
        InventorySystem.UseItem(id);
    }

    public bool HasItem(int id)
    {
        return InventorySystem.HasItem(id); ;
    }

    public CharacterData GetCharacter(int id)
    {
        return Party.GetMember(0);
    }

    //フラグ操作
    public bool GetFlag(string flagName)
    {
        return FlagManager.GetValue(flagName);
    }

    public void SetFlag(string flagName, bool value)
    {
        FlagManager.SetFlag(flagName, value);
    }

    //戦闘操作
    public void Battle(int id)
    {
        StartCoroutine(enumerator());
        IEnumerator enumerator()
        {
            EncounterController.Encount(id);
            BattleController.isBattle = true;
            yield return new WaitUntil(() => BattleController.isBattle == false);
            LuaScript.instance.Resume();
            yield break;
        }
    }

    public Player GetPlayer()
    {
        return null;
    }

    public GameObject GetNPC()
    {
        return LuaScript.instance.evtObj;
    }

    public GameObject FindNPC(string name)
    {
        return GameObject.Find(name);
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
