﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

/*
 * フィールド場で呪文・アイテムを使用するためのクラスです
 */
public class FieldEffect : MonoBehaviour
{
    public CharacterWindow characterWindow;
    public ItemWindow itemWindow;
    public SpellWindow spellWindow;

    public Spell spellData;
    //public PlayerCharacter spellOwner;
    public CharacterData spellOwner;
    //public PlayerCharacter spellTarget;
    public CharacterData spellTarget;

    public AudioSource SE_Audio;
    public AudioClip notUse;
    public AudioClip healClip;

    public void UseItem(Item itemData)
    {
        if (!itemWindow.CanUse(itemData))
        {
            NotExcute();
            return;
        }

        itemWindow.Close();

        //List<PlayerCharacter> party = PlayerParty.Instance.partyMember;
        List<CharacterData> party = Party.GetMember();

        MenuWindow.instance.menuGuide.Show(itemData.itemName + "使用対象を選んでください");

        //複数対象の処理
        if (itemData.targetRange == TargetRange.全体)
        {
            characterWindow.SelectAll();
            characterWindow.ClearLisner();
            characterWindow.AddLisner((int index) =>
            {
                AllItemExecut(itemData);
                MenuWindow.instance.menuGuide.Hide();
            });
        }
        else
        {
            characterWindow.Select(0);
            characterWindow.ClearLisner();
            characterWindow.AddLisner((int index) =>
            {
                CharacterData playerCharacter = party[index];
                ItemExecut(itemData, playerCharacter);
                MenuWindow.instance.menuGuide.Hide();
            });
        }
    }

    public void UseSpell(Spell spellData, CharacterData owner)
    {
        if (!spellWindow.CanFieldSpell(spellData))
        {
            NotExcute();
            return;
        }

        //MP消費
        this.spellData = spellData;
        this.spellOwner = owner;

        MenuWindow.instance.menuGuide.Show(spellData.skillName + "使用対象を選んでください");

        List<CharacterData> party = Party.GetMember();
        //複数対象の処理
        if (spellData.targetRange == TargetRange.全体)
        {
            characterWindow.SelectAll();
            characterWindow.ClearLisner();
            characterWindow.AddLisner((int index) =>
            {
                AllSpellExecut(spellData);
                MenuWindow.instance.menuGuide.Hide();
            });
        }
        else
        {
            characterWindow.Select(0);
            characterWindow.ClearLisner();
            characterWindow.AddLisner((int index) =>
            {
                CharacterData playerCharacter = party[index];
                SpellExecut(spellData, playerCharacter);
                MenuWindow.instance.menuGuide.Hide();
            });
        }
    }

    //public void UseSpell(SpellData spellData, PlayerCharacter owner)
    //{
    //    if (!spellWindow.CanFieldSpell(spellData))
    //    {
    //        NotExcute();
    //        return;
    //    }

    //    //MP消費
    //    this.spellData = spellData;
    //    this.spellOwner = owner;

    //    MenuWindow.instance.menuGuide.Show(spellData.skillName + "使用対象を選んでください");

    //    List<PlayerCharacter> party = PlayerParty.Instance.partyMember;
    //    //複数対象の処理
    //    if (spellData.targetRange == TargetRange.全体)
    //    {
    //        characterWindow.SelectAll();
    //        characterWindow.ClearLisner();
    //        characterWindow.AddLisner((int index) =>
    //        {
    //            AllSpellExecut(spellData);
    //        });
    //    }
    //    else
    //    {
    //        characterWindow.Select(0);
    //        characterWindow.ClearLisner();
    //        characterWindow.AddLisner((int index) =>
    //        {
    //            PlayerCharacter playerCharacter = party[index];
    //            SpellExecut(spellData, playerCharacter);
    //        });
    //    }
    //}

    private void AllItemExecut(Item itemData)
    {
        //List<PlayerCharacter> party = PlayerParty.Instance.partyMember;
        List<CharacterData> party = Party.GetMember();
        foreach (var item in party)
        {
            ItemExecut(itemData, item);
        }
    }

    private async void ItemExecut(Item itemData, CharacterData taregt)
    {
        //アイテムの消費
        InventorySystem.Remove(itemData);

        List<Effect> commandEffect = itemData.effects;
        foreach (var item in commandEffect)
        {
            DoEffect(item, taregt);
        }
        await Task.Delay(1000);
        itemWindow.Open();
    }

    private void AllSpellExecut(Spell spellData)
    {
        //List<PlayerCharacter> party = PlayerParty.Instance.partyMember;
        List<CharacterData> party = Party.GetMember();
        foreach (var item in party)
        {
            SpellExecut(spellData, item);
        }
    }

    private void SpellExecut(Spell spellData, CharacterData taregt)
    {
        //Mpの消費
        //spellOwner.GainMp(this.spellData.mp);
        //マイナスに鳴る
        spellOwner.status.mp -= (this.spellData.mp);

        List<Effect> commandEffect = spellData.effects;
        foreach (var item in commandEffect)
        {
            DoEffect(item, taregt);
        }

        //MP不足で連続で使用できないとき
        if (spellOwner.status.mp <= spellData.mp)
        {
            spellWindow.ShowSpellList(spellOwner);
        }
    }

    //private void SpellExecut(SpellData spellData, PlayerCharacter taregt)
    //{
    //    //Mpの消費
    //    spellOwner.GainMp(this.spellData.mp);

    //    List<CommandEffect> commandEffect = spellData.effects;
    //    foreach (var item in commandEffect)
    //    {
    //        DoEffect(item, taregt);
    //    }

    //    //MP不足で連続で使用できないとき
    //    if (spellOwner.status.mp <= spellData.mp)
    //    {
    //        spellWindow.ShowSpellList(spellOwner);
    //    }
    //}

    public void DoEffect(Effect commandEffect, CharacterData target)
    {
        switch (commandEffect)
        {
            //回復効果
            case HealEffect healEffect:
                //target.Recover(healEffect.healAmount);
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

        //効果音再生
        SE_Audio.PlayOneShot(healClip);

        //ステータス更新
        characterWindow.UpdateShow();
    }

    //public void DoEffect(CommandEffect commandEffect, PlayerCharacter target)
    //{
    //    switch (commandEffect)
    //    {
    //        //回復効果
    //        case HealEffect healEffect:
    //            target.Recover(healEffect.healAmount);
    //            break;
    //        //蘇生効果
    //        case ResuscitationEffect resuscitationEffect:
    //            if (target.status.hp >= 0) return;
    //            target.Recover(target.status.maxHp / resuscitationEffect.healRate);
    //            break;
    //        default:
    //            Debug.Log("not effect");
    //            break;
    //    }

    //    //効果音再生
    //    SE_Audio.PlayOneShot(healClip);

    //    //ステータス更新
    //    characterWindow.UpdateShow();
    //}

    public void NotExcute()
    {
        //効果が仕様できない
        SE_Audio.PlayOneShot(notUse);
    }
}
