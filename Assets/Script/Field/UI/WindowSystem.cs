using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Menuを全体管理するクラス
public class WindowSystem : MonoBehaviour
{
    public GameObject MenuWindow;
    public static bool canOpen = true;
    public static WindowSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canOpen) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!MenuWindow.activeSelf) MenuWindow.SetActive(true);
            //if (MenuWindow.activeSelf) MenuWindow.SetActive(false);
            //else MenuWindow.SetActive(true);
        }
    }

    public void Open()
    {
        PlayerInteract.InteractableStart();
    }

    public void Close()
    {
        PlayerInteract.InteractableEnd();
    }
}

//＊Menuを外から呼び出します
//Menuを表示している間、移動等の入力を制限します

//何がだめかわかったこのクラスMenuの親になっているのが紛らわしい
//このクラスはUIと親子関係はなく協力関係である　初心者がよくやってしまう
//menuWindowをプレファブ参照するべき
