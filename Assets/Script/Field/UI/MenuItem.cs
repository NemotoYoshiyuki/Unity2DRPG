using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEditor;

public class MenuItem : Button
{
    public int index;
    public TextMeshProUGUI text;
    public event Action<int> onHover;
    public event Action<int> onLeftClick;
    public event Action<int> onRightClick;

    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    // this method called by mouse-pointer enter the object.
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (onHover != null) onHover.Invoke(index);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        //Use this to tell when the user left-clicks on the Button
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onLeftClick?.Invoke(index);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            onRightClick.Invoke(index);
        }
    }

    public void AddRegister(Action<int> action)
    {
        onLeftClick = action;
    }

    public void Release()
    {
        onLeftClick = null;
    }
}

#if UNITY_EDITOR
[CanEditMultipleObjects, CustomEditor(typeof(MenuItem), true)]
public class ButtonExEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        this.serializedObject.Update();
        this.serializedObject.ApplyModifiedProperties();
    }
}
#endif