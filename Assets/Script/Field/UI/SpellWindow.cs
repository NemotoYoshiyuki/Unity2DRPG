using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SpellWindow : BaseWindow
{

    public GameObject list;
    public SpellSlot spellSlotPrefab;
    private List<SpellSlot> spellSlots = new List<SpellSlot>();

    public TextMeshProUGUI spellDescription;
    public CharacterSelect selectTarget;
    public FieldEffect fieldEffect;

    private PlayerCharacter owner;
    private List<PlayerCharacter> party;
    private SpellData hoverItem;
    private SpellData selectedItem;

    public Action OnCancel;
    public override void Open()
    {
        UserSelect();
        MenuWindow.instance.currentWindow = this;
    }

    public void UserSelect()
    {
        ////ターゲットリストを表示
        party = PlayerParty.instance.partyMember;
        MenuWindow.instance.sideMenu.Lock();
        ////ボタンが押されたら
        selectTarget.Select((int index) =>
        {
            owner = party[index];
            ShowSpellList(owner);
            selectTarget.Release();
        });

        ////キャンセルが押されたら
        OnCancel = () =>
        {
            MenuWindow.instance.currentWindow = MenuWindow.instance;
            MenuWindow.instance.sideMenu.Unlock();
            selectTarget.Release();
        };
    }

    public void ShowSpellList(PlayerCharacter owner)
    {
        //アイテムリストを表示
        gameObject.SetActive(true);

        List<SpellData> spellDatas = owner.GetSpells();

        for (int i = 0; i < spellDatas.Count; i++)
        {
            SpellSlot spellSlot = Instantiate(spellSlotPrefab);
            spellSlot.index = i;
            spellSlot.spell = spellDatas[i];
            spellSlot.owner = this;

            spellSlot.text.SetText(spellDatas[i].skillName);
            spellSlot.transform.SetParent(list.transform);

            spellSlots.Add(spellSlot);
        }

        //キャンセルが押されたら
        OnCancel = () =>
        {
            HideSpellList();
            UserSelect();
        };
    }

    public void HideSpellList()
    {
        spellSlots.ForEach(x => Destroy(x.gameObject));
        spellSlots.Clear();
        gameObject.SetActive(false);
    }

    public override void Cancel()
    {
        OnCancel.Invoke();
    }
   

    public void ObjectHoveredEnter(SpellSlot spellSlot)
    {
        hoverItem = spellSlot.spell;
        spellDescription.SetText(hoverItem.description);
    }

    public void ObjectOnClick(SpellSlot spellSlot)
    {
        selectedItem = spellSlot.spell;
        HideSpellList();
        fieldEffect.UseSpell(selectedItem, owner);

        OnCancel = () =>
        {
            ShowSpellList(owner);
        };
    }
}
