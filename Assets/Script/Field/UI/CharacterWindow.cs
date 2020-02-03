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
        OffSelect();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show()
    {
        member = PlayerParty.instance.partyMember;
        foreach (var item in member)
        {
            CharacterSlot _characterSlot = Instantiate(characterSlot);
            _characterSlot.playerCharacter = item;
            _characterSlot.Show();

            _characterSlot.transform.parent = list.transform;

            characterSlots.Add(_characterSlot);
        }
    }

    //ターゲット処理
    private Action<int> action;
    public void OnSelect(Action<int> onClick)
    {
        action = onClick;

        for (int i = 0; i < characterSlots.Count; i++)
        {
            MenuItem item = characterSlots[i].gameObject.GetComponent<MenuItem>();
            item.index = i;
            item.enabled = true;
            item.onLeftClick += onClick;
        }
    }

    public void OffSelect()
    {
        foreach (var slot in characterSlots)
        {
            MenuItem item = slot.gameObject.GetComponent<MenuItem>();
            item.enabled = false;
            item.onLeftClick -= action;
        }
    }

    public void Select(Action<int> onClick)
    {
        Show();
        OnSelect(onClick);
    }
}
