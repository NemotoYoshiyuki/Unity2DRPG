using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palsy : StatusEffect
{
    public override int id => (int)StatusEffectType.まひ;
    public override string alimentName => "まひ";

    public override string addMessage => $"{owner.CharacterName}の　からだがしびれて\nうごけなくなった！";

    public override string keepMessage => $"{owner.CharacterName}は　からだがしびれて　うごけない！";

    public override string refreshMessage => $"{owner.CharacterName} の　しびれがきえた";

    public Palsy(BattleCharacter owner, int counter) : base(owner, counter)
    {

    }

    public override void OnAdd()
    {
        //行動不可能フラグを建てる
        owner.canAction = false;
    }

    public override void OnActionBefore()
    {
        BattleDirectorController.Instance.Message(keepMessage);
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
