using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChoiceWindow : MonoBehaviour
{
    public Button choiseButton;
    public Canvas canvas;
    private int index;
    private IEnumerator choise;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Choice("はい","いいえ"));
    }

    // Update is called once per frame
    void Update()
    {

    }



    public IEnumerator Choice(string yse, string no)
    {
        bool a = false;
        Button _yse = Instantiate(choiseButton).GetComponent<Button>();
        Button _no = Instantiate(choiseButton).GetComponent<Button>();

        _yse.GetComponentInChildren<Text>().text = yse;
        _yse.transform.parent = canvas.transform;
        _no.transform.parent = canvas.transform;
        _no.GetComponentInChildren<Text>().text = no;

        _yse.onClick.AddListener(() =>
        {
            index = 1;
            a = true;
            Close();
            
        });
        _no.onClick.AddListener(() =>
        {
            index = 2;
            a = true;
            Close();
        });

        yield return new WaitUntil(() => a == true);
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

    public IEnumerator Choice(Action c1, Action c2)
    {
        yield break;
    }
}
