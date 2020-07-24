using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelector : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(OnRendering());
        IEnumerator OnRendering()
        {
            //Buttonのレンダリング完了するまで待つ必要がある
            yield return new WaitForEndOfFrame();
            //すでにセレクト状態のボタンはSelect()メソッドを使用しても何起きない
            //そのため、一度ボタンのセレクトを解除する必要がある
            EventSystem.current.SetSelectedGameObject(null);
            button.Select();
            yield return null;
        }
    }
}
