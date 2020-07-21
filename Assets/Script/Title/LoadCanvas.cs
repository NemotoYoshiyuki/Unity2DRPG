using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadCanvas : BaseWindow
{
    public SaveSlot[] saveSlot;

    public override void Open()
    {
        base.Open();
        Show();
        saveSlot[0].GetComponent<Button>().Select();
    }

    public override void Close()
    {
        base.Close();
    }

    public override void Cancel()
    {
        Close();
    }

    public void Show()
    {
        for (int i = 0; i < saveSlot.Length; i++)
        {
            SaveSlot m_saveSlot = saveSlot[i];
            int fileNumber = i + 1;
            if (SaveSystem.ExistsSaveFile(fileNumber) == false)
            {
                //saveSlot[i].gameObject.GetComponent<Button>().interactable = false;
                continue;
            }
            SaveData saveData = SaveSystem.LoadFile(fileNumber);
            m_saveSlot.sceneName.text = saveData.gameData.sceneName;
            //誤差あり
            System.TimeSpan ts = new System.TimeSpan(0, 0, (int)saveData.gameData.playTime);
            m_saveSlot.playTime.text = ts.ToString();
        }
    }

    public void Load(int fileNumber)
    {
        if (SaveSystem.ExistsSaveFile(fileNumber) == false) return;
        GameController.Load(fileNumber);
        SaveData.GameData gameData = SaveSystem.saveData.gameData;

        //プレイヤーの位置を指定してシーンを再生
        SceneController.Transition(gameData.sceneName, gameData.playerPotion);
    }
}
