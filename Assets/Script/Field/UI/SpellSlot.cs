using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SpellSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public int index;
    public SpellWindow owner;
    public Spell spell;
    public TextMeshProUGUI text;
    public AudioSource seAudio;

    public SelectableButton selectable;

    private void Start()
    {
        selectable = GetComponent<SelectableButton>();
    }

    public void SetUp(Spell spellData)
    {
        this.spell = spellData;
        SetText(spellData.skillName);
    }

    public void SetText(string text)
    {
        this.text.SetText(text);
    }

    public void Invalid()
    {
        Color subtractionColor = new Color(0.6f, 0.6f, 0.6f, 0);
        selectable.image.color -= subtractionColor;
        //icon.color -= subtractionColor;
        text.color -= subtractionColor;

        //クリック動作を変更する
        selectable.onClick.RemoveAllListeners();
        selectable.onClick.AddListener(() => seAudio.Play()); ;
    }

    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。SelectableSelectable
    // this method called by mouse-pointer enter the object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        //owner.ObjectHoveredEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectable.interactable == false) return;
        //owner.ObjectOnClick(this);
    }
}
