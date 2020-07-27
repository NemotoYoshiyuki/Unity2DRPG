using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : StatusEffect
{
    public override int id => (int)StatusEffectType.どく;
    public override string alimentName => "どく";
    public override string addMessage => $"{owner.CharacterName}は　どくにおかされた";
    public override string keepMessage => $"{owner.CharacterName}は どくにおかさている";
    public override string refreshMessage => $"{owner.CharacterName} の　からだにまわっている　どくがきえた！";

    public Poison(BattleCharacter owner, int counter) : base(owner, counter)
    {

    }

    public override void OnTurnEnd()
    {
        //ターン経過で解消
        base.OnTurnEnd();

        if (counter < 0) return;
        //毒ダメージ(最大HPの10%)
        float damage = owner.status.maxHp * 0.1f;
        BattleDirectorController.Instance.Damage(owner, (int)damage, keepMessage);
    }
}
