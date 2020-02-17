using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEffect : MonoBehaviour
{
    public CharacterSelect selectTargetWindow;
    public CharacterWindow characterWindow;

    public SpellData spellData;
    public PlayerCharacter spellOwner;
    public PlayerCharacter spellTarget;

    public void UseItem(ItemData itemData)
    {
        List<PlayerCharacter> party = PlayerParty.instance.partyMember;

        //複数対象の処理
        if (itemData.targetRange == TargetRange.全体)
        {
            selectTargetWindow.SelectAll((int index) =>
            {
                foreach (var item in party)
                {
                    Execut(itemData, item);
                }
            });
        }
        else
        {
            //キャラクターを選択して実行
            selectTargetWindow.Select((int index) =>
            {
                PlayerCharacter playerCharacter = party[index];
                Execut(itemData, playerCharacter);
            });
        }
    }

    public void UseSpell(SpellData spellData, PlayerCharacter owner)
    {
        //MP消費
        this.spellData = spellData;
        this.spellOwner = owner;

        List<PlayerCharacter> party = PlayerParty.instance.partyMember;
        //複数対象の処理
        if (spellData.targetRange == TargetRange.全体)
        {
            selectTargetWindow.SelectAll((int index) =>
            {
                foreach (var item in party)
                {
                    Execut(spellData, item);
                }
            });
        }
        else
        {
            selectTargetWindow.Select((int index) =>
             {
                 PlayerCharacter target = PlayerParty.instance.partyMember[index];
                 Execut(this.spellData, target);
             });
        }
    }

    private void Execut(ItemData itemData, PlayerCharacter taregt)
    {
        //アイテムの消費
        GameController.GetInventorySystem().UseItem(itemData);

        List<CommandEffect> commandEffect = itemData.effects;
        foreach (var item in commandEffect)
        {
            DoEffect(item, taregt);
        }
    }

    private void Execut(SpellData itemData, PlayerCharacter taregt)
    {
        //Mpの消費
        spellOwner.GainMp(this.spellData.mp);

        List<CommandEffect> commandEffect = spellData.effects;
        foreach (var item in commandEffect)
        {
            DoEffect(item, taregt);
        }
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
