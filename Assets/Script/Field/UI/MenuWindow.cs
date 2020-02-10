using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        cancels.Push(this);
        AddHistory(new Undo(()=>Close()));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("キャンセル");
            Back();
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

    void ICancel.Undo()
    {
        Close();
    }


}
