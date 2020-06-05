using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDirector : BattleDirector
{
    private int damage;
    private BattleCharacter target;
    private string message = null;

    public DamageDirector(int damage, BattleCharacter target)
    {
        this.damage = damage;
        this.target = target;
    }

    public DamageDirector(int damage, BattleCharacter target, string message)
    {
        this.damage = damage;
        this.target = target;
        this.message = message;
    }

    public override IEnumerator Do()
    {
        //Hpを減らす
        target.GainHp(damage);

        //メッセージ表示
        if (message != null)
        {
            Debug.Log(message);
            yield return BattleMessage.Show(message);
        }
        yield break;
    }
}
