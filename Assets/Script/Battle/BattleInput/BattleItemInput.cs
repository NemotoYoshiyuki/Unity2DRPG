using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleItemInput : BattleCommandInput
{
    public Button button;
    public SelectButtonBox selectButtonBox;

    public override void Close()
    {
        base.Close();
        selectButtonBox.Close();
    }

    public void Show(List<ItemData> itemDatas)
    {
        foreach (var item in itemDatas)
        {
            Button _button = Instantiate(button);
            _button.GetComponentInChildren<TextMeshProUGUI>().text = item.name;
            _button.onClick.AddListener(() => OnClick(item));
            selectButtonBox.AddRegister(_button);
        }

        selectButtonBox.Show();
    }

    public void OnClick(ItemData itemData)
    {
        ItemCommand item = new ItemCommand(itemData);
        BattleInputController.instance.SelectCommand(item);
    }

}
