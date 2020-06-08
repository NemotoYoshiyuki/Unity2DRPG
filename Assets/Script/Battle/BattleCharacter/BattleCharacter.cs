using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    //バフリスト
    public Status status;
    //状態異常
    public StatusEffect statusEffect;
    //行動可能フラグ
    public bool canAction = true;

    //強化
    public BattleStaus battleStaus;//戦闘用ステータス

    public virtual string CharacterName { get; set; }

    public void SetUp()
    {
        this.battleStaus = new BattleStaus(status);
    }

    public void Recover(int amount)
    {
        status.hp = Mathf.Clamp(status.hp + amount, 0, status.maxHp);
    }

    //状態異常にする
    public void AddStatusEffect(StatusEffect statusEffect)
    {
        this.statusEffect = statusEffect;
    }

    //状態異常を治療する
    public void Treatment()
    {
        statusEffect = null;
    }

    public void AddBuff(Buff buff)
    {
        battleStaus.buffs.Add(buff);
        battleStaus.Update();
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
        //状態異常を解除
        statusEffect = null;
    }

    public void GainHp(int value)
    {
        status.hp = Mathf.Clamp(status.hp - value, 0, status.maxHp);
    }

    public void GainMp(int value)
    {
        status.mp = Mathf.Clamp(status.mp - value, 0, status.maxMp);
    }
}
