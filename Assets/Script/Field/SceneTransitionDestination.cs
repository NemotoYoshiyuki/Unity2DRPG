using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

//移動先の座標を示すオブジェクト
public class SceneTransitionDestination : MonoBehaviour
{
    public enum DestinationTag
    {
        A, B, C, D, E, F, G,
    }

    public DestinationTag destinationTag;    // This matches the tag chosen on the TransitionPoint that this is the destination for.

    void OnDrawGizmos()
    {
#if UNITY_EDITOR

        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.fontStyle = FontStyle.Bold;
        style.fontSize = 20;
        style.normal.textColor = Color.red;

        UnityEditor.Handles.Label(transform.position, destinationTag.ToString(), style);
#endif
    }
}