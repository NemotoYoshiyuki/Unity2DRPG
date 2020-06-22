using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public int index;
    public Image icon;
    public Item item;
    public TextMeshProUGUI text;
    public AudioSource seAudio;
    public SelectableButton selectable;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetUp(Item item)
    {
        this.item = item;
        text.SetText(item.itemName);
    }

    public void Invalid()
    {
        Color subtractionColor = new Color(0.6f, 0.6f, 0.6f, 0);
        selectable.image.color -= subtractionColor;
        //icon.color -= subtractionColor;
        text.color -= subtractionColor;

        //クリック動作を変更する
        //selectable.onClick.RemoveAllListeners();
        //selectable.onClick.AddListener(() => seAudio.Play()); ;
    }
}
