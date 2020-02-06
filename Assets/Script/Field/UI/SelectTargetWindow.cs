using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectTargetWindow : MonoBehaviour
{
    public List<MenuItem> menuItems;
    private event Action<int> onLeftClick;

    public void AddRegister(Action<int> action)
    {
        onLeftClick = action;

        foreach (var item in menuItems)
        {
            item.AddRegister(action); ;
        }
    }


}
