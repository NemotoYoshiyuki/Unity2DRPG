using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SideMenu : MonoBehaviour, ICancel
{
    public List<Button> sideButtons;

    private void Start()
    {
        sideButtons[0].Select();
    }

    public void Lock()
    {
        StartCoroutine(Locking());
    }

    //Buttonを半透明にしないで現在の状態のままinteractable = false;に変更します
    private IEnumerator Locking()
    {
        MenuWindow.AddHistory(this);
        MenuWindow.AddHistory(new Undo(() => Unlock()));

        //Buttonの色がFadeを完了されるまで待ちます
        //fadeDurationの値によってFadeにかかる時間は変わりますデフォルトでは0.1fです
        yield return new WaitForSeconds(0.1f);

        foreach (var item in sideButtons)
        {
            //Buttonの現在の色を取得します
            //Buttonを押したときに変化するselectedColorはCanvasRendererによって色を重ねているためです
            CanvasRenderer canvasRenderer = item.gameObject.GetComponent<CanvasRenderer>();
            Color color = canvasRenderer.GetColor();

            ColorBlock colorBlock = item.colors;
            colorBlock.disabledColor = color;
            item.colors = colorBlock;

            item.interactable = false;
        }

        yield break;
    }

    public void Unlock()
    {
        sideButtons.ForEach(x => x.interactable = true);
    }

    void ICancel.Undo()
    {
        Unlock();
    }
}
