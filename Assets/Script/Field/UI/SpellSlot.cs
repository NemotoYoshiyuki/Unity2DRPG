using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SpellSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public int index;
    public SpellWindow owner;
    public SpellData spell;
    public TextMeshProUGUI text;

    public SelectableItem selectable;

    private void Start()
    {
        selectable = GetComponent<SelectableItem>();
    }

    public void SetText(string text)
    {
        this.text.SetText(text);
    }

    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。SelectableSelectable
    // this method called by mouse-pointer enter the object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        owner.ObjectHoveredEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectable.interactable == false) return;
        owner.ObjectOnClick(this);
    }
}
