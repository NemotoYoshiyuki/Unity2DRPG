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
}