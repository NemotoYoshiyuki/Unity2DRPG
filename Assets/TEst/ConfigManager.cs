using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    //シングルトン

    //ゲームによって項目を増やしてください
    //全部staticです
    public static float bgmVolume;
    public static float seVolume;
    public static float msSpeed;//メッセージ速度

    //セーブ対象です
    public void Save()
    {

    }

    public void Load()
    {

    }
}
