using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SideMenu : MonoBehaviour
{
    public List<SelectableButton> sideButtons;

    private void OnEnable()
    {
        sideButtons[0].Select();
    }

    public void Lock()
    {
        foreach (var item in sideButtons)
        {
            Debug.Log(item);
            //item.clickable = false;
        }
    }

    public void Unlock()
    {
        foreach (var item in sideButtons)
        {
            //sideButtons[0].Select();
            //item.clickable = true;
            //キー操作無効
        }
    }
}
