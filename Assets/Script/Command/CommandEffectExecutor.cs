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

    private IEnumerator PhysicalAttack(PhysicalAttackEffect physicalAttackEffect, BattleCharacter owner, BattleCharacter target)
    {
        float damageRate = physicalAttackEffect.damageRate;
        int damage = BattleFormula.PhysicalAttackDamage(owner.status,target.status);
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
}
