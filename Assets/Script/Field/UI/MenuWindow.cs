using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;

public class MenuWindow : BaseWindow, ICancel
{
    public static MenuWindow instance;
    public BaseWindow currentWindow;
    public CharacterWindow CharacterWindow;

    private List<BaseWindow> history;
    private Stack<ICancel> cancels = new Stack<ICancel>();
    private Stack<Undo> undos = new Stack<Undo>();

    // Start is called before the first frame update
    void Start()
    {
        currentWindow = this;
        instance = this;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        // When the Menu starts, set the rendering to target 20fps
        //OnDemandRendering.renderFrameInterval = 3;

        cancels.Push(this);
        AddHistory(new Undo(() => Close()));
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
        //一つ前の状態に戻る
        //currentWindow.Close();
        //ICancel cancel = cancels.Pop();
        //Debug.Log(cancel.ToString());
        //cancel.Undo();
        //cancels.Pop().Undo();

        //Undo
        undos.Pop().Excute();
    }

    public void AddHistory(BaseWindow baseWindow)
    {
        history.Add(baseWindow);
    }

    public static void AddHistory(ICancel baseWindow)
    {
        Debug.Log(baseWindow);
        instance.cancels.Push(baseWindow);
    }

    public static void AddHistory(Undo undo)
    {
        Debug.Log("add");
        instance.undos.Push(undo);
    }

    public static void AddHistory(Action action)
    {
        Debug.Log("add");
        instance.undos.Push(new Undo(action));
    }

    void ICancel.Undo()
    {
        Close();
    }


}
