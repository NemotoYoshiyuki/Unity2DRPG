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
        //未実装
    }

    private bool ExistsSaveData()
    {
        return false;
    }
}
