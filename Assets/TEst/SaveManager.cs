using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public const string path ="";

    public void Save()
    {

    }

    public void Load()
    {
        //なければエラー
    }

    //非同期 データベースの読み込みが完了しているか。
    //セーブファイルが(ひとつでも)存在するか。
    //指定したセーブファイルID からデータを読み込む。
    //maxSavefiles セーブ可能なファイルの最大数を返す。
    //selectSavefileForNewGame  新規ゲーム用のセーブファイルを選択。
}
