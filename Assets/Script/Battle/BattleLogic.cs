using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ロジッククラス
//参考 http://www.lancarse.co.jp/blog/?p=194
public class BattleLogic : MonoBehaviour
{
    //バトル演出キュー
    public Queue queue = new Queue();

    private BattleCharacter tempPlayerChar;//計算用プレイヤー
    private BattleCharacter tempEnemyChar;//計算用敵プレイヤー

    //効果音
    public AudioClip se;
    //演出クラスで参照するよう　アニメーションクリップに含めるべきなのか？
    public AudioClip attackSE;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Play()
    {
        //バトル演出キューを再生します
    }
    //行動開始前に一回だけ
    public void Attack(AnimationClip animationClip, string message)
    {
        //アニメ再生を追加
        //メッセージ再生を追加
    }

    public void Critical(AnimationClip animationClip)
    {
        //アニメ再生を追加
        //メッセージ再生を追加
    }

    //適切に何回でも
    public void Miss()
    {
        //SE再生を追加
        //メッセージ再生を追加
    }

    public void Avoidance(BattleCharacter character)
    {
        //SE再生を追加
        //メッセージ再生を追加
    }

    //計算結果を受け取って演出を生成します
    public void DamageLogic(int damage)
    {
        //ダメージを受ける前に発動する処理が存在したら
        //例）アストロン・魔法反射・かばう
        //妥協）Retrunする仕様とする
        if (IsBeginDamage())
        {
            BeginDamage();
            return;
        }

        if (damage < 0)
        {
            //ダメージ0以下
            //◆◆◆◆に　ダメージを　あたえられない！
        }
        else
        {
            //ダメージ演出を生成します
            //体力の変更を通知UIを更新します

            //対象のHPが0になった
            if (IsDead())
            {
                DeadLogic();
            }

            //ダメージを受けたときに発動する処理が存在したら
            if (IsCounter())
            {
                //庇う）物理ダメージのみ
                Counter();
            }
        }

        //対象のHPが0になった
        if (IsDead())
        {
            DeadLogic();
        }
    }

    public void Counter()
    {
        //反撃可能なら
        //CounterAttarkLogic();
    }

    //ダメージを受けたあと反撃攻撃
    public void CounterAttarkLogic()
    {
        //反撃ダメージ
        int damage = 0;
        DamageLogic(damage);
    }

    //庇う
    public void Protect()
    {
        //ownerを味方に変更
        //ダメージ再計算
        int damage = 0;
        DamageLogic(damage);
    }

    /*
     * Public Class 魔法反射 :状態{
     * 
     *  Public bool IsBegin(ダメージ情報){
     *  //ダメージの情報を受取る
     *  //攻撃が呪文のときかつ回復・補助ではないとき
     *  }
     *  
     *  Public void Begin(){
     *      //確定）演出を直接キューに入れない　ダメージ処理で死亡をすり抜けてしまうから
     *      //ターゲットを変更してダメージを計算し直す
            int damage = 0;
            DamageLogic(damage);
     *  }
     * }
     */
    public void BeginDamage()
    {
        //例）アストロン・魔法反射・かばう

        //アタッチメントパターンになる？
        //状態変化クラスでなんかする？　ステート？
    }

    //計算結果を受け取って演出を生成します
    public void DamageLogic()
    {
        //ダメージ演出を生成します

        //体力の変更を通知UIを更新します
    }

    //生存結果を受け取って演出を生成します
    public void DeadLogic()
    {
        //死亡処理を行います
        //UIを更新します
    }

    //魔法反射
    public void ReflectionLogic()
    {
        //回復・補助は透過

        //反射演出をキューに追加　ターゲットとダメージを引数に

        //ダメージ演出をキューに追加
        //ターゲットを変更してダメージを計算し直す
        int damage = 0;
        DamageLogic(damage);
    }

    public void 状態異常()
    {
        //状態異常を生成します
    }

    //判定
    public bool IsCritical(int rate)
    {
        return false;
    }

    public bool IsHit(int hitRate)
    {
        //目に砂が入った
        //霧がでている
        return true;
    }

    public bool IsAvoidance()
    {
        //敵のみかわしを参照
        return false;
    }

    public bool IsDead()
    {
        return false;
    }

    //ダメージを受ける前に発動するなにかがあるとき
    public bool IsBeginDamage()
    {
        //アストロン・魔法反射・かばう

        //条件分岐をなくしたい
        return false;
    }

    public bool IsCounter()
    {
        return false;
    }

    //計算

    //攻撃基礎ダメージ
    public int BasicPhysicalDamageCalculation(BattleCharacter character, BattleCharacter character1)
    {
        return 10;
    }

    //確定ダメージ
    public int PhysicalDamageCalculation(BattleCharacter character, BattleCharacter character1, float rate)
    {
        //バイキルトはダメージを受けたとき関数で処理
        //防御を考慮する必要がある　ダメージ軽減率
        //バフを考慮する必要がある
        return 10;
    }

    //魔法ダメージ計算
    public int SpellDamageCalculation()
    {
        return 10;
    }

    //その他
    public void Message(string message)
    {
        //バトルメッセージを表示
    }

    /*
     * 考え中
     * 麻痺とアストロンの演出差し込み
     * 防御されたときのダメージ計算と演出
     * つまりダメージを受けたとき関数の演出差し込み
     * アストロンは解除呪文は有効
     * 
     * ダメージの算出方法
     * 状態異常の演出
     * 妥協可能）反射の全体反射のときどうする 行動開始前まで巻き戻ってターゲットを変更してやり直す
     * キューに追加して実行するクラスは非Monoになる必要なアセットの参照方法
     * 技の結果構造体

    /*
     * 演出一覧
     * 状態異常　毒・麻痺
     * 回復　吸収　状態異常
     * ダメージ　クリティカル
     * 逃げる
     * ステータス変動
     * 技効果　いろいろ
     * 変身
     * 状態変化　反射？ 無敵
     */
    //非Monoビヘイビアになる

    /*
     * クリティカル100(確定)　成功率50
     * 行動宣言　きゅうしょを　ねらった！
     * 行動結果　きゅうしょをちょくげき！ねらいは　はずれてしまった！
     * 行動結果通知　に　▲の　ダメージ！
     */
}
