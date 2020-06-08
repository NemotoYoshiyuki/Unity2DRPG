using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : StatusEffect
{
    public override int id => (int)StatusEffectType.どく;
    public override string alimentName => "どく";
    public override string onAdd => $"{owner.CharacterName}は　どくにおかされた";
    public override string onGrant => $"{owner.CharacterName}は どくにおかさている";
    public override string resolution => $"{owner.CharacterName} の　からだにまわっている　どくがきえた！";

    public Poison(int counter, BattleCharacter owner) : base(counter, owner)
    {

    }

    public override void OnTurnEnd()
    {
        //ターン経過で解消
        base.OnTurnEnd();
        if (counter == 0)
        {
            return;
        }

        //毒ダメージ
        int damage = 100;

        _BattleLogic.Instance.Damage(owner,damage,onGrant);
    }
}
