using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public void OpenWindow()
    {
        party = PlayerParty.instance.partyMember;
        selectTarget.Select((int index) =>
        {
            owner = party[index];
            OpenSpellList(owner);
            selectTarget.Release();
        });
    }

    public void OpenSpellList(PlayerCharacter owner)
    {
        gameObject.SetActive(true);

        List<SpellData> spellDatas = owner.GetSpells();

        for (int i = 0; i < spellDatas.Count; i++)
        {
            SpellSlot _menuItem = Instantiate(spellSlotPrefab);
            _menuItem.index = i;
            _menuItem.spell = spellDatas[i];
            _menuItem.owner = this;

            _menuItem.text.SetText(spellDatas[i].skillName);
            _menuItem.transform.SetParent(list.transform);

            spellSlots.Add(_menuItem);
        }
    }

    public void CloseSpellList()
    {
        foreach (var item in spellSlots)
        {
            Destroy(item.gameObject);
        }
        spellSlots.Clear();
        gameObject.SetActive(false);
    }

    public void ObjectHoveredEnter(SpellSlot spellSlot)
    {
        hoverItem = spellSlot.spell;
        spellDescription.SetText(hoverItem.description);
    }

    public void ObjectOnClick(SpellSlot spellSlot)
    {
        selectedItem = spellSlot.spell;
        fieldEffect.UseSpell(selectedItem, owner);
        CloseSpellList();
    }
}
