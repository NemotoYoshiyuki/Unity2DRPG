using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChoiceWindow : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;
    private int index;

    // Start is called before the first frame update
    void Start()
    {

    }

    public IEnumerator Choice(string yse = "はい", string no = "いいえ")
    {
        index = 0;
        bool isChoise = false;

        gameObject.SetActive(true);
        yesButton.GetComponentInChildren<TextMeshProUGUI>().SetText(yse);
        noButton.GetComponentInChildren<TextMeshProUGUI>().SetText(no);

        void OnClick()
        {
            isChoise = true;
            Close();
        }

        yesButton.onClick.AddListener(() =>
        {
            index = 1;
            OnClick();
        });

        noButton.onClick.AddListener(() =>
        {
            index = 2;
            OnClick();
        });

        yield return new WaitUntil(() => isChoise == true);
        yield break;
    }

    public int Result()
    {
        return index;
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
}
