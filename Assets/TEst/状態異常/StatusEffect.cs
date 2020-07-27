using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    public virtual int id { get; }
    public virtual string alimentName { get; }
    public virtual string addMessage { get; }
    public virtual string keepMessage { get; }
    public virtual string refreshMessage { get; }

    //継続ターン数
    public int counter;
    public BattleCharacter owner;//この状態異常にかかっているキャラ

    //コンストラクタ
    public StatusEffect(BattleCharacter owner, int counter)
    {
        this.counter = counter;
        this.owner = owner;

        owner.onActionBefore += OnActionBefore;
        BattleController.instance.onDamage += OnDamage;
        BattleController.instance.onTurnEnd += OnTurnEnd;
    }

    public virtual void OnAdd()
    {

    }

    public virtual void OnActionBefore()
    {

    }

    public virtual void OnDamage()
    {

    }

    public virtual void OnTurnEnd()
    {
        CountDown();
    }

    public virtual void Refresh()
    {
        owner.onActionBefore -= OnActionBefore;
        BattleController.instance.onDamage -= OnDamage;
        BattleController.instance.onTurnEnd -= OnTurnEnd;
    }

    private void CountDown()
    {
        counter--;
        if (counter < 0)
        {
            BattleDirectorController.Instance.RemoveStatusEffect(owner);
        }
    }

    //解除条件　睡眠ならダメージを受けたとき
    //解除タイミング
    //解除メソッド

    //重ねがけ バフなら一段回上昇　猛毒　効果延長

}

//ダメージ計算に影響する強化・弱化
//対象に影響を与える　状態異常
//対象以外にも影響を与える　状態変化

/*
 * 
特定のステータスが増減する（【スカラ】【ルカニ】による守備力アップ・ダウンや【ピオリム】による素早さアップなど）
特定の攻撃で与えるダメージや回復量が増減する（【バイキルト】【ちからため】【テンション変化状態】など）
特定の攻撃で受けるダメージが増減する（【フバーハ】による【ブレス攻撃軽減】など）
特定の攻撃を無効化または反射する（【マホカンタ】による【呪文反射】など）アストロン　反撃
HPやMPが徐々に回復または減少する（【リホイミ】や【毒】【猛毒】など）
行動不能になる（【眠り】【麻痺】【１ターン休み】など）
コマンド入力を受け付けず、味方に攻撃する（【混乱】【魅了】など）
呪文や特技の使用が制限される（【呪文封じ状態】【特技封印】など）
行動の対象を相手または味方にする(挑発　身代わり)
別な存在に変身し、能力が変わったり独自のパターンで行動したりするようになる（【モシャス】【ドラゴラム】など）
 */

//強化・弱化・キャンセル・縛り・スリップダメージ
//睡眠・麻痺　フラグが必要？
//混乱・魅力　フラグが必要？
//封印　フラグが必要？
//全部行動をキャンセルする動作を行う

//状態異常の特性として効果によって"特定"の状態異常は解除されなければいけない
//どうやって状態異常を指定するか、前はジェネリックだが解除メソッドで条件分岐が発生した
//Public Void 解除(毒)

//状態異常中は他の状態異常にかからない、もしくは優先度が存在する

//状態異常はスクリプトテーブルで作成
//インスペクターで効果を作成するとき設定できる

//問題
//キャンセル動作の実装
//Inspectorで効果の作成
//Bit演算で管理すれば混乱以外を解除することはできるのか