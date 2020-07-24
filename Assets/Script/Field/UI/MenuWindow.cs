using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;

public class MenuWindow : BaseWindow
{
    public static MenuWindow instance;
    public BaseWindow focusWindow;
    //すべての子要素を参照
    [Tooltip("各メニューのCanvas")]
    public SideMenu sideMenu;
    public ItemWindow itemWindow;
    public SpellWindow spellWindow;
    public SaveWindow saveWindow;
    public MenuGuide menuGuide;

    private void OnEnable()
    {
        //PlayerMovement.canMove = false;
        PlayerInteract.InteractableStart();
    }

    private void OnDisable()
    {
        //PlayerMovement.canMove = true;
        PlayerInteract.InteractableEnd();
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        focusWindow = this;


        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        // メニューが起動したら、レンダリングのターゲットを 20fps に設定します
        OnDemandRendering.renderFrameInterval = 3;
    }

    // Update is called once per frame
    void Update()
    {

        if (!Input.anyKey)
        {
            // マウスやタッチによる入力がない場合は（3 フレームごとに）20fps に戻します
            OnDemandRendering.renderFrameInterval = 3;
            return;
        }
        //何も入力がない
        //retun 

        // マウスクリックやタッチ操作が検出されたら、（フレームごとに）60fps でレンダリングします
        OnDemandRendering.renderFrameInterval = 1;


        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("キャンセル");
            Back();
        }
    }

    public override void Open()
    {
        base.Open();
        //フレームレートを下げる
    }

    public override void Close()
    {
        base.Close();
        //フレームレートを戻す
    }

    public void Back()
    {
        focusWindow.Cancel();
    }

    public override void Cancel()
    {
        Close();
    }
}
