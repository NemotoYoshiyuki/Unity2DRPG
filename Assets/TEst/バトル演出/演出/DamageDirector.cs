using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDirector : BattleDirector
{
    private BattleCharacter target;
    private int damage;
    private string message = null;

    public DamageDirector(BattleCharacter target, int damage)
    {
        this.damage = damage;
        this.target = target;
    }

    public DamageDirector(BattleCharacter target, int damage, string message)
    {
        this.damage = damage;
        this.target = target;
        this.message = message;
    }

    public override IEnumerator Execute()
    {
        //Hpを減らす
        target.ReceiveDamage(damage);

        //メッセージ表示
        if (message != null)
        {
            yield return BattleMessage.Show(message);
        }
        yield break;
    }
}
