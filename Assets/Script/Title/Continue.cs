using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Continue : MonoBehaviour
{
    public Button continueButton;

    private void Start()
    {
        if (!ExistsSaveData())
        {
            continueButton.gameObject.SetActive(false);
        }
    }

    public void PlayContinue()
    {
        GameController.Instance.Load();
        SaveData.GameData gameData = SaveSystem.saveData.gameData;

        //プレイヤーの位置を指定してシーンを再生
        SceneController.Instance.Transition(gameData.sceneName, gameData.playerPotion);
    }

    private bool ExistsSaveData()
    {
        return SaveSystem.ExistsSaveData();
    }
}