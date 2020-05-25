using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEditor;

public class SelectableButton : Button
{
    public int index;
    public Action<int> onPointerClick;
    public Action onHover;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        clickEvent.Invoke(index);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
    }

    //キーボード入力に対応
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        onHover?.Invoke();
    }

    //キーボード入力に対応
    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        clickEvent?.Invoke(index);
    }

    public void ChangeOpacity(float alpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }

    public void ChangeDisabledColor(Color32 color32)
    {
        ColorBlock colorBlock = colors;
        colorBlock.disabledColor = color32;
        colors = colorBlock;
    }

    public Color GetCurrentColor()
    {
        Color color = new Color();
        switch (currentSelectionState)
        {
            case SelectionState.Normal:
                color = colors.normalColor;
                break;
            case SelectionState.Highlighted:
                color = colors.highlightedColor;
                break;
            case SelectionState.Pressed:
                color = colors.pressedColor;
                break;
            case SelectionState.Selected:
                color = colors.selectedColor;
                break;
            case SelectionState.Disabled:
                color = colors.disabledColor;
                break;
            default:
                break;
        }
        return color;
    }

    private bool CanClick()
    {
        //連続入力の対策
        return true;
    }

    public void FindSelectable(IReadOnlyList<Selectable> selectables)
    {
        Navigation m_navigation = navigation;
        float up = Mathf.Infinity;
        float down = Mathf.Infinity;
        float right = Mathf.Infinity;
        float left = Mathf.Infinity;

        for (int i = 0; i < selectables.Count; i++)
        {
            Selectable selectable = selectables[i];

            if (selectable == this) continue;

            RectTransform selectableRect = selectable.transform as RectTransform;
            RectTransform rectTransform = gameObject.transform as RectTransform;

            //距離
            float norm = (rectTransform.position - selectableRect.position).sqrMagnitude;
            //方向
            Vector3 direction = (rectTransform.position - selectableRect.position).normalized;

            //上
            if (direction.y < 0)
            {
                if (norm < up)
                {
                    up = norm;
                    m_navigation.selectOnUp = selectable;
                }
            }

            //下
            if (direction.y > 0)
            {
                if (norm < down)
                {
                    down = norm;
                    m_navigation.selectOnDown = selectable;
                }
            }

            //右
            if (direction.x < 0)
            {
                if (norm < right)
                {
                    right = norm;
                    m_navigation.selectOnRight = selectable;
                }
            }

            //左
            if (direction.x > 0)
            {
                if (norm < left)
                {
                    left = norm;
                    m_navigation.selectOnLeft = selectable;
                }
            }
        }
        m_navigation.mode = Navigation.Mode.Explicit;
        navigation = m_navigation;
    }

    //後で見直す
    public class ClickEvent : UnityEvent<int> //引数の型を指定しておく
    {

    }
    private ClickEvent m_clickEvent;
    public ClickEvent clickEvent
    {
        get
        {
            if (m_clickEvent == null)
            {
                m_clickEvent = new ClickEvent();
            }
            return m_clickEvent;
        }
    }
}
