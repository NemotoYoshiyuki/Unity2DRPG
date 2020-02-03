using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWindow : BaseWindow
{
    public static MenuWindow instance;
    public BaseWindow currentWindow;
    public CharacterWindow CharacterWindow;

    // Start is called before the first frame update
    void Start()
    {
        currentWindow = this;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("キャンセル");
            currentWindow.Close();
        }
    }

    public void Back()
    {
        //一つ前の状態に戻る
    }
}
