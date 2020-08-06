using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCommand : BattleCommand
{
    public Item item;

    public ItemCommand(Item itemData)
    {
        this.item = itemData;
    }

    public ItemCommand(Item itemData, BattleCharacter owner, BattleCharacter target) : base(owner, target)
    {
        this.item = itemData;
    }

    public ItemCommand(BattleCharacter owner, List<BattleCharacter> target) : base(owner, target)
    {

    }

    public override IEnumerator Execution()
    {
        string itemName = item.itemName;
        string itemMessage = owner.CharacterName + "は" + itemName + "をつかった";
        BattleDirectorController.Instance.Message(itemMessage);
        //yield return StartCoroutine(message.ShowAuto(itemMessage));

        //行動開始時のメッセージ表示
        if (item.actionMessage != string.Empty)
        {
            string actionMessage = item.actionMessage;
            BattleDirectorController.Instance.Message(actionMessage);
        }

        InventorySystem.Remove(item);

        yield break;
    }

    public override TargetType GetTargetType()
    {
        return new TargetType(item.targetUnit, item.targetRange);
    }

    public override List<Effect> GetEffect()
    {
        return item.effects;
    }

    public override Command GetCommand()
    {
        return item;
    }
}
