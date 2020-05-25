using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//UI_Item -- 項目
public class SelectItemList : MonoBehaviour
{
    public int lastIndex;
    public int index;

    public List<SelectItem> selectItems;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Register(SelectItem selectItem)
    {
        selectItems.Add(selectItem);
    }

    public void Select(int index)
    {

    }

    public void AddLisner()
    {

    }

    public void ClearLisner()
    {

    }

    //子要素のボタンを押せないようにする
    public void Lock()
    {

    }

    public void UnLock()
    {

    }

    public void Refresh()
    {

    }

    //ボタンフォーカス
}
