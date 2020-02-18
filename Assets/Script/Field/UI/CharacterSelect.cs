using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CharacterSelect : MonoBehaviour
{
    private CharacterWindow characterWindow;
    private List<SelectItem> selectItems = new List<SelectItem>();


    private void Start()
    {
        characterWindow = GetComponent<CharacterWindow>();
    }

    public void Select(Action<int> action)
    {
        List<CharacterSlot> characterSlots = characterWindow.characterSlots;
        for (int i = 0; i < characterSlots.Count; i++)
        {
            SelectItem selectItem = characterSlots[i].gameObject.GetComponent<SelectItem>();
            selectItem.index = i;
            selectItem.enabled = true;
            selectItem.AddRegister(action);
            selectItems.Add(selectItem);
        }
        selectItems[0].Select();
    }

    //すべての要素を選択状態にする
    public void SelectAll(Action<int> action)
    {
        List<CharacterSlot> characterSlots = characterWindow.characterSlots;
        for (int i = 0; i < characterSlots.Count; i++)
        {
            SelectItem selectItem = characterSlots[i].gameObject.GetComponent<SelectItem>();
            selectItem.index = i;
            selectItem.enabled = true;
            selectItem.AddRegister(action);
            selectItems.Add(selectItem);
            selectItem.Select();
        }
    }

    public void Release()
    {
        foreach (var item in selectItems)
        {
            item.Release();
            item.enabled = false;
        }
    }

    public void Flash(PlayerCharacter player,Color color,float time)
    {
        List<CharacterSlot> characterSlots = characterWindow.characterSlots;
        CharacterSlot characterSlot = characterSlots.First(x=>x.playerCharacter == player);
        Flash flash = characterSlot.GetComponent<Flash>();
        flash.FlashStart(color,time);
    }
}
