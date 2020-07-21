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

    public void Show(List<Item> itemDatas)
    {
        foreach (var item in itemDatas)
        {
            if (CanUseItem(item) == false) continue;
            Button _button = Instantiate(button);
            _button.GetComponentInChildren<TextMeshProUGUI>().text = item.name;
            _button.onClick.AddListener(() => OnClick(item));
            selectButtonBox.AddRegister(_button);
        }

        selectButtonBox.Show();
    }

    public bool CanUseItem(Item item)
    {
        return item.useType == UseType.戦闘中 || item.useType == UseType.いつでも;
    }

    public void OnClick(Item itemData)
    {
        ItemCommand item = new ItemCommand(itemData);
        BattleInputController.instance.SelectCommand(item);
    }

}
