using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Action OnEventStart;
    public Action OnEvent;
    public Action OnEventEnd;

    public IEnumerator Run()
    {
        OnEventStart?.Invoke();//制限入力済みとする

        //イベント実行
        //コルーチン
        //yield return OnEvent?.Invoke();

        OnEventEnd?.Invoke();//制限入力済みとする

        yield break;
    }
}
