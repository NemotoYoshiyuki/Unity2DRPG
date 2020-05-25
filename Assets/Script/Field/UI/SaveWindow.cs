using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveWindow : BaseWindow
{
    public override void Open()
    {
        base.Open();
        MenuWindow.instance.focusWindow = this;
    }

    public override void Close()
    {
        base.Close();
    }

    public override void Cancel()
    {
        MenuWindow.instance.focusWindow = MenuWindow.instance;
        Close();
    }

    public void Save()
    {
        GameController.Instance.Save();
    }

    public void Load()
    {
        GameController.Instance.Load();
    }
}
