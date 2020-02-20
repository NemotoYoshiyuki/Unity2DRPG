using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveWindow : BaseWindow
{
    public override void Open()
    {
        base.Open();
        MenuWindow.instance.currentWindow = this;
    }

    public override void Close()
    {
        base.Close();
    }

    public override void Cancel()
    {
        MenuWindow.instance.currentWindow = MenuWindow.instance;
        Close();
    }
}
