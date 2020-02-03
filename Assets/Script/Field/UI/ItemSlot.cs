using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : Button
{
    public int index;
    public ItemWindow owner;
    public Image _image { get { return GetComponent<Image>(); } }

    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    // this method called by mouse-pointer enter the object.
    public override void OnPointerEnter(PointerEventData eventData)
    {
        //_image.color = Color.red;
        owner.ObjectHoveredEnter(this);
    }
}
