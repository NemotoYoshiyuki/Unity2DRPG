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
        StartCoroutine(OnRendering());
        IEnumerator OnRendering()
        {
            //Buttonのレンダリング完了するまで待つ必要がある
            yield return new WaitForEndOfFrame();
            //すでにセレクト状態のボタンはSelect()メソッドを使用しても何起きない
            //そのため、一度ボタンのセレクトを解除する必要がある
            EventSystem.current.SetSelectedGameObject(null);
            sideButtons[0].Select();
            yield return null;
        }
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
