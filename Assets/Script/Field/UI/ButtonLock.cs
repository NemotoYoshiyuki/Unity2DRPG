using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonLock : MonoBehaviour,IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        Lock();
        Image image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Button button;
    ColorBlock colorBlock;

    //Imageの色を変更
    public void Lock()
    {
        colorBlock = button.colors;
        colorBlock.disabledColor = Color.black;
        button.interactable = false;
        button.colors = colorBlock;
    }


    public void UnLock()
    {

    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        button.OnSelect(eventData);
    }
}
