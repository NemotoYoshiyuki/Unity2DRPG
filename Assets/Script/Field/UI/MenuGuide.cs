using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuGuide : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Show(string message)
    {
        gameObject.SetActive(true);
        text.SetText(message);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        text.SetText(string.Empty);
    }
}
