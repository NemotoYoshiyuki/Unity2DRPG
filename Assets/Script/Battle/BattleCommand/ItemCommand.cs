using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCommand : BattleCommand
{
    public ItemData item;

    public ItemCommand(ItemData itemData)
    {
        this.item = itemData;
    }

    public ItemCommand(ItemData itemData, BattleCharacter owner, BattleCharacter target) : base(owner, target)
    {
        this.item = itemData;
    }

    public ItemCommand(BattleCharacter owner, List<BattleCharacter> target) : base(owner, target)
    {

    }

    public override TargetType GetTargetType()
    {
        return new TargetType(item.targetUnit, item.targetRange);
    }

    public override List<CommandEffect> GetEffect()
    {
        return item.effects;
    }
}
