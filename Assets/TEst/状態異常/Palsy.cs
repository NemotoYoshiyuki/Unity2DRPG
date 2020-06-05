using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palsy : StatusEffect
{
    public override int id => (int)StatusEffectType.まひ;
    public override string alimentName => "まひ";

    public override string onAdd => $"{owner.CharacterName}の　からだがしびれて\nうごけなくなった！";

    public override string onGrant => $"{owner.CharacterName}は　からだがしびれて　うごけない！";

    public override string resolution => $"{owner.CharacterName} の　しびれがきえた";

    public Palsy(int counter, BattleCharacter owner) : base(counter, owner)
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
        _BattleLogic.Instance.End();
    }

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
    }

    public override void Refresh()
    {
        owner.canAction = true;
        base.Refresh();
    }
}
