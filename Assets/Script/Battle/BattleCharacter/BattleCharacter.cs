﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    private Status basicStatus;//基礎ステータス
    public Status BasicStatus => basicStatus;
    public Status status => battleStaus.Status;
    public BattleStaus battleStaus;//戦闘用ステータス

    public int hate = 0;//狙われ率
    public float damageIncrease = 1f;//ダメージ加算倍率
    public float damageReduction = 1f;//ダメージ減算倍率
    public bool canAction = true;//行動可能フラグ
    public bool isCounter = false;//カウンターフラグ
    public bool isSpellLimit = false;//呪文行動制限

    public virtual string CharacterName { get; set; }

    public void SetBasicStatus(Status status)
    {
        this.basicStatus = status.Copy();
    }

    public void SetUp()
    {
        this.battleStaus = new BattleStaus(basicStatus);
    }

    public void GainHp(int value)
    {
        status.hp = Mathf.Clamp(status.hp - value, 0, status.maxHp);
    }

    public void GainMp(int value)
    {
        status.mp = Mathf.Clamp(status.mp - value, 0, status.maxMp);
    }

    public void Recover(int amount)
    {
        status.hp = Mathf.Clamp(status.hp + amount, 0, status.maxHp);
    }

    public void ReceiveDamage(int damage)
    {
        status.hp = Mathf.Clamp(status.hp - damage, 0, status.maxHp);
        if (IsDead()) OnDead();
    }

    //状態異常にする
    public void AddStatusEffect(StatusEffect statusEffect)
    {
        this.battleStaus.statusEffect = statusEffect;
    }

    public void RemoveStatusEffect()
    {
        this.battleStaus.statusEffect = null;
    }

    public StatusEffect GetStatusEffect()
    {
        return battleStaus.statusEffect;
    }

    public void AddBuff(Buff buff)
    {
        battleStaus.buffs.Add(buff);
        battleStaus.StatusUpdate();
    }

    public void RemoveBuff()
    {
        battleStaus.DeleteBuff();
        battleStaus.StatusUpdate();
    }

    public void StatusUpdate()
    {
        battleStaus.StatusUpdate();
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
        RemoveStatusEffect();
    }
}
