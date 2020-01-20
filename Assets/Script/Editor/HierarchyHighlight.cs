using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class HierarchyHighlight : MonoBehaviour
{
    [InitializeOnLoadMethod]
    static void DrawComponentIcons()
    {
        EditorApplication.hierarchyWindowItemOnGUI += (instanceID, selectionRect) =>
        {
            //インスタンス ID からゲームオブジェクトを取得
            var go = (GameObject)EditorUtility.InstanceIDToObject(instanceID);

            if (go == null)
                return;

            var position = new Rect(selectionRect)
            {
                width = 16,
                height = 16,
                x = Screen.width - 20
            };

            foreach (var component in go.GetComponents<Component>())
            {
                //Transform は全ゲームオブジェクトにあるので
                //無駄な情報なため表示しない
                if (component is HierarchyDrawer)
                {
                    HierarchyDrawer hirarchyPaint = go.GetComponent<HierarchyDrawer>();
                    var _color = hirarchyPaint.color;

                    var pos = selectionRect;
                    pos.x = 0;
                    pos.xMax = selectionRect.xMax;

                    var color = GUI.color;
                    GUI.color = new Color(_color.r, _color.g, _color.b, 0.1f);
                    GUI.Box(pos, string.Empty);
                    GUI.color = color;
                    continue;
                }
            }
        };
    }
}
