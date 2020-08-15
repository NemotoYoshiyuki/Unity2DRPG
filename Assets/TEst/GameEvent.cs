using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvent : MonoBehaviour
{
    public Action onEventStart;
    public Action onEventEnd;
    protected static GameEvent instance;
    public static GameEvent Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            instance = FindObjectOfType<GameEvent>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private static void Create()
    {
        instance = new GameObject("GameEvent").AddComponent<GameEvent>();
    }

    public static void Execute(IEnumerator routine)
    {
        Instance.StartCoroutine(Instance.Run(routine));
    }

    protected IEnumerator Run(IEnumerator routine)
    {
        //イベント開始処理
        OnEventStart();

        //遅延実行 キー入力の干渉をさける
        yield return new WaitForEndOfFrame();
        //yield return new WaitForSeconds(0.5f);

        //イベント実行
        yield return StartCoroutine(routine);

        //イベント終了処理
        OnEventEnd();

        yield break;
    }

    protected void OnEventStart()
    {
        onEventStart?.Invoke();

        //イベント実行中は行動が制限される
        PlayerMovement.canMove = false;
        PlayerInteract.canInteract = false;
        WindowSystem.canOpen = false;
    }

    protected void OnEventEnd()
    {
        onEventEnd?.Invoke();

        //イベントが終了したので行動の制限を解除する
        PlayerMovement.canMove = true;
        PlayerInteract.canInteract = true;
        WindowSystem.canOpen = true;

        //メッセージウィンドを閉じる
    }
}
