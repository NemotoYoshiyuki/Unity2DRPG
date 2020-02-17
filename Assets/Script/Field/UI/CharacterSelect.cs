using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public void Release()
    {
        foreach (var item in selectItems)
        {
            item.Release();
            item.enabled = false;
        }
    }
}
