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

    //public void Run()
    //{
    //    StartCoroutine(OnExcute());
    //}

    //public abstract IEnumerator Hoge();

    //private IEnumerator OnExcute()
    //{
    //    PlayerInteract.InteractableStart();
    //    yield return Hoge();//イベントの終了を待機
    //    PlayerInteract.InteractableEnd();
    //    yield break;
    //}
}