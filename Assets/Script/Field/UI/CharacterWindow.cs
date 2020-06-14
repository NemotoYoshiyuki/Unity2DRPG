using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class CharacterWindow : BaseWindow
{
    //public List<PlayerCharacter> member;
    public List<CharacterData> member;
    public GameObject list;
    public CharacterSlot characterSlotPrefab;

    public List<CharacterSlot> characterSlots;
    public List<SelectableButton> selectItems;

    public int index;//選択中の項目番号

    //
    public SelectItemList selectItemList;

    // Start is called before the first frame update
    void OnEnable()
    {
        Show();
    }

    private void Start()
    {

    }

    public void Show()
    {
        CloseWindow();

        member = GameController.GetParty().characterDatas;

        for (int i = 0; i < member.Count; i++)
        {
            CharacterSlot characterSlot = Instantiate(characterSlotPrefab);
            characterSlot.playerCharacter = member[i];
            characterSlot.Show();

            SelectableButton selectItem = characterSlot.selectableButton;
            selectItem.index = i;

            characterSlot.transform.SetParent(list.transform);

            characterSlots.Add(characterSlot);

            selectItems.Add(characterSlot.selectableButton);
        }

        RegisterNavigation();
    }

    public void Init()
    {

    }

    public void UpdateShow()
    {
        foreach (var item in characterSlots)
        {
            item.Show();
        }
    }

    public void CloseWindow()
    {
        foreach (var item in characterSlots)
        {
            Destroy(item.gameObject);
        }
        characterSlots.Clear();
    }

    public void AddLisner(UnityAction<int> unityAction)
    {
        foreach (var item in selectItems)
        {
            item.clickEvent.AddListener(unityAction);
        }
    }

    public void ClearLisner()
    {
        foreach (var item in selectItems)
        {
            item.clickEvent.RemoveAllListeners();
        }
    }

    //特定のボタンを選択状態にする
    public void Select(int index)
    {
        foreach (var item in selectItems)
        {
            if (item.index == index)
            {
                item.Select();
                break;
            }
        }
    }

    //未対応
    //すべてのボタンを選択状態にする
    public void SelectAll()
    {
        foreach (var item in selectItems)
        {
            //item.focus = true;
            item.Select();
        }
    }

    private void RegisterNavigation()
    {
        StartCoroutine(CreateNavigation());
    }

    //Button.Navigation.modeのExplicitに適切なButtonを割り当てます
    private IEnumerator CreateNavigation()
    {
        //Layoutgroupを使用しているため、レイアウトの構築が完了するまで待つ必要があります
        yield return new WaitForEndOfFrame();

        foreach (var item in selectItems)
        {
            item.FindSelectable(selectItems);
        }
        yield break;
    }
}
