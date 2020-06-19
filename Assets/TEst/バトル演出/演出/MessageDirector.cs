using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDirector : BattleDirector
{
    private string message;

    public MessageDirector(string message)
    {
        this.message = message;
    }

    public override IEnumerator Execute()
    {
        yield return BattleMessage.Show(message);
        yield break;
    }
}
