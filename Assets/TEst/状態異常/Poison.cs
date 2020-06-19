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
        //毒ダメージ
        int damage = 100;
        BattleDirectorController.Instance.Damage(owner, damage, keepMessage);

        //ターン経過で解消
        base.OnTurnEnd();
    }
}
