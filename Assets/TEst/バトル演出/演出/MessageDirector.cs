using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDirector : BattleDirector
{
    public string message;

    public MessageDirector(string message)
    {
        this.message = message;
    }

    public override IEnumerator Do()
    {
        Debug.Log(message);
        yield return BattleMessage.Show(message);
        yield break;
    }
}
