using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;

public class MenuWindow : BaseWindow
{
    public static MenuWindow instance;
    public BaseWindow currentWindow;
    public SideMenu sideMenu;
    public MenuGuide menuGuide;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentWindow = this;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        // When the Menu starts, set the rendering to target 20fps
        OnDemandRendering.renderFrameInterval = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            // If the mouse button or touch detected render at 60 FPS (every frame).
            OnDemandRendering.renderFrameInterval = 1;

            Debug.Log("キャンセル");
            Back();
        }
        else
        {
            // If there is no mouse and no touch input then we can go back to 20 FPS (every 3 frames).
            OnDemandRendering.renderFrameInterval = 3;
        }
    }

    public void Back()
    {
        currentWindow.Cancel();
    }

    public override void Cancel()
    {
        Close();
    }
}
