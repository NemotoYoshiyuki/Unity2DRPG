using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    //バフリスト
    public Status status;

    public virtual string CharacterName { get; set; }

    public void Recover(int amount)
    {
        status.hp = Mathf.Clamp(status.hp + amount, 0, status.maxHp);
    }

    //状態異常を治療する
    public void Treatment()
    {

    }

    public void ReceiveDamage(int damage)
    {
        status.hp = Mathf.Clamp(status.hp - damage, 0, status.maxHp);
        if (IsDead()) OnDead();
    }

    //コマンド入力が可能な状態なのか
    public bool CanCommandInput()
    {
        return true;
    }

    public bool IsDead()
    {
        return status.hp <= 0;
    }

    //味方キャラクターと敵キャラクターで死亡した時の処理が違うためvirtualで定義
    public virtual void OnDead()
    {

    }

    public void GainMp(int value)
    {
        status.mp = Mathf.Clamp(status.mp - value, 0, status.maxMp);
    }
}
