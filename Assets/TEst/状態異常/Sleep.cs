using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : StatusEffect
{
    public override int id => (int)StatusEffectType.すいみん;
    public override string alimentName => "ねむり";

    public override string onAdd => $"{owner.CharacterName}を ねむらせた！";

    public override string onGrant => $"{owner.CharacterName}は ねむっている！";

    public override string resolution => $"{owner.CharacterName} は　めを　さました！";

    public Sleep(int counter, BattleCharacter owner) : base(counter, owner)
    {

    }

    public override void OnAdd()
    {
        //行動不可能フラグを建てる
        base.OnAdd();
        owner.canAction = false;
    }

    public override void OnActionBefore()
    {
        //その後、行動を行うことはできない
        _BattleLogic.Instance.Message(onGrant);
    }

    public override void OnDamage(EffectInfo info)
    {
        //攻撃を受けたときこの状態異常は解除される
        Refresh();
    }

    public override void OnTurnEnd()
    {
        //ターン経過で状態異常が解除される
        base.OnTurnEnd();
    }

    public override void Refresh()
    {
        owner.canAction = true;
        base.Refresh();
    }
}
