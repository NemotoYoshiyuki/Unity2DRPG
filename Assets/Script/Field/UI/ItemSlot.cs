using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public int index;
    public ItemWindow owner;
    public TextMeshProUGUI text;

    public void SetText(string text)
    {
        this.SetText(text);
    }

    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    // this method called by mouse-pointer enter the object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        owner.ObjectHoveredEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        owner.ObjectOnclic(this);
    }
}
