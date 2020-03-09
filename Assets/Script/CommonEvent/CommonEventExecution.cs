using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommonEventExecution : MonoBehaviour
{
    //イベント起動条件
    //起動するイベント
    //条件を満たしていなければ非表示

    //セルフ変数
    public bool on = false;//このオブジェクトを表示するか
    public int a = 0;//イベント進行度
    public bool self0 = false;
    public bool self1 = false;

    public UnityEvent OnStart;

    private void Start()
    {
        gameObject.SetActive(on);
    }
}

/*
 * レベル１０になったら仲間になるよ
 *  ifで判定する
 *  以下のとき　もっときたえろ
 *  以上のとき　お前強そうだな
 *  「～が仲間になった」
 *  パーティーに入れる
 *  これ以降非表示になる
 */