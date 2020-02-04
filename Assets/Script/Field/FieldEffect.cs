using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEffect : MonoBehaviour
{
    public CharacterWindow characterWindow;
    public MenuItem menuItem;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Item(ItemData itemData)
    {
        characterWindow.OnSelect((int index) =>
        {
            PlayerCharacter playerCharacter = PlayerParty.instance.partyMember[index];
            UseItem(itemData, playerCharacter);
        });
    }

    private void UseItem(ItemData itemData, PlayerCharacter taregt)
    {
        //アイテムを消費

        CommandEffect commandEffect = itemData.effects[0];
        DoEffect(commandEffect, taregt);
    }

    public void Spell(SpellData spellData, PlayerCharacter owner)
    {
        //MP消費
        owner.GainMp(spellData.mp);

        characterWindow.OnSelect((int index) =>
        {
            PlayerCharacter playerCharacter = PlayerParty.instance.partyMember[index];
            UseItem(spellData, playerCharacter);
        });

        //DoEffect(spellData.effects[0], target);
    }

    private void UseItem(SpellData itemData, PlayerCharacter taregt)
    {
        //アイテムを消費

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
        characterWindow.Show();
        Debug.Log(target + commandEffect.ToString());
    }
}
