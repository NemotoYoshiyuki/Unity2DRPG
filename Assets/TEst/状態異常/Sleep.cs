using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : StatusEffect
{
    public override int id => (int)StatusEffectType.すいみん;
    public override string alimentName => "ねむり";

    public override string addMessage => $"{owner.CharacterName}を ねむらせた！";

    public override string keepMessage => $"{owner.CharacterName}は ねむっている！";

    public override string refreshMessage => $"{owner.CharacterName} は　めを　さました！";

    public Sleep(BattleCharacter owner, int counter) : base(owner, counter)
    {

    }

    public override void OnAdd()
    {
        base.OnAdd();
        //行動不可能フラグを建てる
        owner.canAction = false;
    }

    public override void OnActionBefore()
    {
        BattleDirectorController.Instance.Message(keepMessage);
    }

    public override void OnDamage()
    {
        //攻撃を受けたときこの状態異常は解除される
        BattleDirectorController.Instance.RemoveStatusEffect(owner);
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
