using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideMenu : MonoBehaviour,ICancel
{
    public List<Button> sideButtons;

    private void Start()
    {
        sideButtons[0].Select();
    }

    public void Lock()
    {
        MenuWindow.AddHistory(this);
        MenuWindow.AddHistory(new Undo(()=>Unlock()));
        sideButtons.ForEach(x => x.interactable = false);
    }

    public void Unlock()
    {
        sideButtons.ForEach(x => x.interactable = true);
    }

    void ICancel.Undo()
    {
        Unlock();
    }
}
