using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterSelect : MonoBehaviour
{
    public List<MenuItem> menuItems;
    private event Action<int> onLeftClick;
    private CharacterWindow characterWindow;

    private void Start()
    {
        characterWindow = GetComponent<CharacterWindow>();
    }

    public void AddRegister(Action<int> action)
    {
        onLeftClick = action;
        menuItems = characterWindow.menuItems;

        foreach (var item in menuItems)
        {
            item.AddRegister(onLeftClick); ;
        }
    }

    public void Select(Action<int> action)
    {
        AddRegister(action);

        foreach (var item in menuItems)
        {
            item.enabled = true;
        }
    }


    public void Release()
    {
        onLeftClick -= onLeftClick;
    }
}
