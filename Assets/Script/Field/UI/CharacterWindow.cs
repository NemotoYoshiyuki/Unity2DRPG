using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterWindow : BaseWindow
{
    public List<PlayerCharacter> member;
    public GameObject list;
    public CharacterSlot characterSlotPrefab;

    public List<CharacterSlot> characterSlots;
    public List<SelectItem> menuItems;

    // Start is called before the first frame update
    void Start()
    {
        Show();
    }

    public void Show()
    {
        CloseWindow();

        member = PlayerParty.instance.partyMember;
        foreach (var item in member)
        {
            CharacterSlot characterSlot = Instantiate(characterSlotPrefab);
            characterSlot.playerCharacter = item;
            characterSlot.Show();

            characterSlot.transform.SetParent(list.transform);

            characterSlots.Add(characterSlot);
        }
    }

    public void UpdateShow()
    {
        foreach (var item in characterSlots)
        {
            item.Show();
        }
    }

    public void CloseWindow()
    {
        foreach (var item in characterSlots)
        {
            Destroy(item.gameObject);
        }
        characterSlots.Clear();
    }
}
