using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterWindow : BaseWindow
{
    public List<PlayerCharacter> member;
    public GameObject list;
    public CharacterSlot characterSlot;

    public List<CharacterSlot> characterSlots;
    public List<MenuItem> menuItems;

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
            CharacterSlot _characterSlot = Instantiate(characterSlot);
            _characterSlot.playerCharacter = item;
            _characterSlot.Show();

            _characterSlot.transform.SetParent(list.transform);

            characterSlots.Add(_characterSlot);
        }

        for (int i = 0; i < characterSlots.Count; i++)
        {
            MenuItem item = characterSlots[i].gameObject.GetComponent<MenuItem>();
            item.index = i;
            item.enabled = true;
            menuItems.Add(item);

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
