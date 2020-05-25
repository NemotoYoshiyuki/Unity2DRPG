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

    public void OnExcute()
    {
        PlayerInteract.InteractableStart();
        OnInteractable();
        PlayerInteract.InteractableEnd();
    }
}