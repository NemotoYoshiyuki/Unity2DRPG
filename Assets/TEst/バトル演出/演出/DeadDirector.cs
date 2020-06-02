using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadDirector : BattleDirector
{
    public BattleCharacter deadCharacter;

    public DeadDirector(BattleCharacter deadCharacter)
    {
        this.deadCharacter = deadCharacter;
    }

    public override IEnumerator Do()
    {
        //死亡処理を行う
        deadCharacter.OnDead();

        yield return BattleMessage.Show(deadCharacter.CharacterName + "は　たおれた!");
        yield break;
    }
}
