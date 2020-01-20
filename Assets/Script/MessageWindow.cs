using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class MessageWindow : MonoBehaviour
{
    public AudioClip click;
    public TextMeshProUGUI text;
    public float messageSpeed = 1;

    public IEnumerator ShowAuto(string text)
    {
        ShowText(text);
        yield return new WaitForSeconds(messageSpeed);
        Close();
    }

    public IEnumerator ShowClick(string text)
    {
        ShowText(text);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Return));
        yield break;
    }

    public void ShowText(string text)
    {
        Open();
        Clear();
        this.text.SetText(text);
    }

    public void Clear()
    {
        text.text = string.Empty;
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
