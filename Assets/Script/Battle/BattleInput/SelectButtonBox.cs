using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButtonBox : BattleCommandInput
{
    public int displayNumber = 8;//表示件数
    public Button nextButton;
    public Button backButton;
    private int page = 1;
    private List<Button> selectButtons = new List<Button>();

    public override void Close()
    {
        page = 1;
        ClearButtons();
    }

    public void Show()
    {
        if (selectButtons == null)
            Debug.LogError("表示するボタンが存在しません");

        UpdatePageButton();
        UpdateItems();
    }

    private void UpdateItems()
    {
        selectButtons.ForEach(x => x.gameObject.SetActive(false));

        int min = (displayNumber * page - displayNumber);
        int max = displayNumber * page;
        max = Mathf.Clamp(max, 0, selectButtons.Count);

        for (int i = min; i < max; i++)
        {
            selectButtons[i].gameObject.SetActive(true);
        }
    }

    private void UpdatePageButton()
    {
        float f = (float)selectButtons.Count / displayNumber;
        if (Math.Ceiling(f) <= page) nextButton.interactable = false;
        else nextButton.interactable = true;

        if (page <= 1) backButton.interactable = false;
        else backButton.interactable = true;
    }

    public void Register(List<Button> selectButtons)
    {
        this.selectButtons = selectButtons;
        foreach (var item in selectButtons)
        {
            item.gameObject.transform.SetParent(gameObject.transform);
        }
    }

    public void AddRegister(Button selectButton)
    {
        selectButton.transform.SetParent(gameObject.transform);
        this.selectButtons.Add(selectButton);
    }

    public void Next()
    {
        page++;
        UpdateItems();
        UpdatePageButton();
    }

    public void Back()
    {
        page--;
        UpdateItems();
        UpdatePageButton();
    }

    private void ClearButtons()
    {
        foreach (var item in selectButtons)
        {
            Destroy(item.gameObject);
        }
        selectButtons.Clear();
    }
}
