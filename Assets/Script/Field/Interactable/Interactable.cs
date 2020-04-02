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

    public void InteractableStart()
    {
        PlayerInput.canMove = false;
        PlayerInteract.canInteract = false;
        WindowSystem.canOpen = false;
    }

    public void InteractableEnd()
    {
        PlayerInput.canMove = true;
        PlayerInteract.canInteract = true;
        WindowSystem.canOpen = true;
    }
}
