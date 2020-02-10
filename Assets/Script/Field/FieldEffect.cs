using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEffect : MonoBehaviour
{
    public CharacterSelect selectTargetWindow;
    public CharacterWindow characterWindow;
    public MenuItem menuItem;

    public SpellData spellData;
    public PlayerCharacter spellOwner;
    public PlayerCharacter spellTarget;

    public void UseItem(ItemData itemData)
    {
        selectTargetWindow.Select((int index) =>
        {
            PlayerCharacter playerCharacter = PlayerParty.instance.partyMember[index];
            Execut(itemData, playerCharacter);
        });

        Debug.Log("s");
        MenuWindow.AddHistory(new Undo(() => selectTargetWindow.Release()));

    }

    public void UseSpell(SpellData spellData, PlayerCharacter owner)
    {
        //MP消費
        this.spellData = spellData;
        this.spellOwner = owner;

        selectTargetWindow.Select((int index) =>
        {
            spellOwner.GainMp(this.spellData.mp);
            PlayerCharacter target = PlayerParty.instance.partyMember[index];
            Execut(this.spellData, target);
        });

        MenuWindow.AddHistory(new Undo(()=> selectTargetWindow.Release()));
    }

    private void Execut(ItemData itemData, PlayerCharacter taregt)
    {
        CommandEffect commandEffect = itemData.effects[0];
        DoEffect(commandEffect, taregt);
    }

    private void Execut(SpellData itemData, PlayerCharacter taregt)
    {
        CommandEffect commandEffect = itemData.effects[0];
        DoEffect(commandEffect, taregt);
    }

    public void DoEffect(CommandEffect commandEffect, PlayerCharacter target)
    {
        switch (commandEffect)
        {
            //回復効果
            case HealEffect healEffect:
                target.Recover(healEffect.healAmount);
                break;
            //蘇生効果
            case ResuscitationEffect resuscitationEffect:
                if (target.status.hp >= 0) return;
                target.Recover(target.status.maxHp / resuscitationEffect.healRate);
                break;
            default:
                Debug.Log("not effect");
                break;
        }
        //ステータス更新
        characterWindow.UpdateShow();
        Debug.Log(target + commandEffect.ToString());
    }
}
