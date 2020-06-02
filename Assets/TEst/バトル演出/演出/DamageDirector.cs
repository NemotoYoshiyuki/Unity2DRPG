using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDirector : BattleDirector
{
    private int damage;
    private BattleCharacter target;

    public DamageDirector(int damage, BattleCharacter target)
    {
        this.damage = damage;
        this.target = target;
    }

    public override IEnumerator Do()
    {
        //Hpを減らす
        target.GainHp(damage);

        //メッセージ表示
        //yield return BattleMessage.Show(target.CharacterName + "は" + damage + "うけた");

        yield break;
    }
}
