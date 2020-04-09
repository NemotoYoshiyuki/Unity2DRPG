using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//このスクリプトをアタッチされたオブジェクトのレイヤーはNPC,Interactableでなくてはいけません
[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    public virtual void OnInteractable()
    {

    }
}
