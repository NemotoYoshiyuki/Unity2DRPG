using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveWindow : BaseWindow
{
    public SaveSlot[] saveSlot;

    public override void Open()
    {
        base.Open();
        MenuWindow.instance.focusWindow = this;
        Show();
        saveSlot[0].GetComponent<Button>().Select();
    }

    public override void Close()
    {
        base.Close();
    }

    public override void Cancel()
    {
        MenuWindow.instance.focusWindow = MenuWindow.instance;
        //TODO:sideButtons[x] xの値変更の可能性有り
        MenuWindow.instance.sideMenu.sideButtons[2].Select();
        Close();
    }

    public void Show()
    {
        for (int i = 0; i < saveSlot.Length; i++)
        {
            int fileNumber = i + 1;
            if (SaveSystem.ExistsSaveFile(fileNumber) == false) continue;
            SaveData saveData = SaveSystem.LoadFile(fileNumber);
            SaveSlot m_saveSlot = saveSlot[i];
            m_saveSlot.sceneName.text = saveData.gameData.sceneName;
            //誤差あり
            System.TimeSpan ts = new System.TimeSpan(0, 0, (int)saveData.gameData.playTime);
            m_saveSlot.playTime.text = ts.ToString();
        }
    }

    public void Save()
    {
        GameController.Save();
    }

    public void Save(int fileNumber)
    {
        GameController.Save(fileNumber);
        //再描写
        Show();
    }

    public void Load()
    {
        GameController.Load();
    }

    public void Load(int fileNumber)
    {
        GameController.Load(fileNumber);
    }
}
