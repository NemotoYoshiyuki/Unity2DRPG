using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//バトルコマンドの効果を処理するクラスです
public class CommandEffectExecutor : MonoBehaviour
{
    public CommandDirectionExecutor directionExecution;
    public MessageWindow message;

    //バトルコマンドの効果を実行します
    public IEnumerator Execution(BattleCommand battleCommand)
    {
        BattleCharacter owner = battleCommand.owner;
        List<BattleCharacter> targets = battleCommand.target;
        TargetUnit targetUnit = battleCommand.GetTargetType().targetUnit;

        var effects = battleCommand.GetEffect();

        foreach (var effect in effects)
        {
            foreach (var target in targets)
            {
                if (!CanEffect(target, targetUnit)) continue;
                yield return StartCoroutine(EffectExecution(effect, owner, target));
            }
        }
        yield break;
    }

    public bool CanEffect(BattleCharacter target, TargetUnit targetUnit)
    {
        if (!target.IsDead()) return true;
        if (target.IsDead() && targetUnit == TargetUnit.死亡者) return true;

        return false;
    }

    //CommandEffectの継承している型によって処理が決まります
    private IEnumerator EffectExecution(CommandEffect effect, BattleCharacter owner, BattleCharacter target)
    {
        switch (effect)
        {
            case PhysicalAttackEffect physicalAttackEffect:
                yield return StartCoroutine(PhysicalAttack(physicalAttackEffect, owner, target));
                break;
            case SpellDamageEffect spellDamageEffect:
                yield return StartCoroutine(SpellDamage(spellDamageEffect, owner, target));
                break;
            case HealEffect healEffect:
                yield return StartCoroutine(Heal(healEffect, owner, target));
                break;
            case ResuscitationEffect resuscitationEffect:
                yield return StartCoroutine(Resuscitation(resuscitationEffect, owner, target));
                break;
            default:
                break;
        }
        yield break;
    }

    //攻撃
    //L miss判定
    //L 会心判定
    //E ～に10ダメージ
    //L倒したのか判定
    //E ～は倒れた
    //L 死亡処理
    //E 死亡演出

    //ギラ
    //E 炎が敵を包み込む
    //戦闘前の演出はすでに行われている

    //E 炎が出る
    //E 音がでる
    //E ～に10ダメージ
    //L ダメージ処理
    //E ～は倒れた
    //L 死亡処理
    //E 死亡演出
    //おしまい

    //ザオリク
    //成功と失敗で演出が分岐する
    //L 成功 or 失敗
    //E 失敗した
    //E 音が出る
    //E ～は生き返った
    //L 復活処理(ステータスの再計算)
    //E 復活演出
    //おしまい

    //メガンテ
    //即死耐性を参照
    //失敗 効かなかった
    //敵を倒した時「は　くだけちった」
    //「は　息絶えた」

    //
    //魔法カウンターなどが途中で挟まることもある

    //会心の一撃は7一般的に貫通と呼ばれるもの

    //バトルコントローラー　回避・ダメージ処理・脂肪処理
    //処理するときにイベントプールにメッセージ表示コマンド等を登録
    //一連のコマンド処理が終了
    //イベントプールのコマンドを再生する

    private IEnumerator PhysicalAttack(PhysicalAttackEffect physicalAttackEffect, BattleCharacter owner, BattleCharacter target)
    {
        float damageRate = physicalAttackEffect.damageRate;
        int damage = BattleFormula.PhysicalAttackDamage(owner.status, target.status);
        damage = (int)(damage * damageRate);

        directionExecution.Hit();

        target.ReceiveDamage(damage);
        yield return StartCoroutine(message.ShowAuto(target.CharacterName + "は" + damage + "うけた"));
        yield break;
    }


    private IEnumerator SpellDamage(SpellDamageEffect spellDamageEffect, BattleCharacter owner, BattleCharacter target)
    {
        int damage = spellDamageEffect.damageAmount;
        target.ReceiveDamage(damage);

        yield return StartCoroutine(message.ShowAuto(target.CharacterName + "は" + damage + "うけた"));
        yield break;
    }

    public IEnumerator Heal(HealEffect healEffect, BattleCharacter owner, BattleCharacter target)
    {
        int healAmount = healEffect.healAmount;
        owner.Recover(healAmount);

        yield return StartCoroutine(message.ShowAuto(target.CharacterName + "は" + healAmount + "かいふくした"));
        yield break;
    }

    private IEnumerator Resuscitation(ResuscitationEffect resuscitation, BattleCharacter owner, BattleCharacter target)
    {
        if (!target.IsDead())
            yield return StartCoroutine(message.ShowAuto("なにもおきなかった"));

        int healAmount = target.status.maxHp / resuscitation.healRate;
        target.Recover(healAmount);
        yield return StartCoroutine(message.ShowAuto(target.CharacterName + "はいきかえった"));
        yield break;
    }

    //計算クラス
    //引数　プレイヤー　対象の敵 受けるダメージ クリティカル　ミス
    public void Damege()
    {
        //プレイヤーが攻撃
        //ミスの場合
        //会心の場合
        //通常のダメージ

        //敵が攻撃
        //ミスの場合
        //痛恨の場合
        //通常のダメージ
    }

    //死亡クラス
    //引数　死亡したプレイヤー
    public void Dead()
    {
        //味方の場合
        //敵の場合
    }

    //メリットは
    //Switch文がなくなるかクラスが見やすくなるならやりたい
}
