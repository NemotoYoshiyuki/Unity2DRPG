using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSpellInput : BattleCommandInput
{
    public Button button;
    public SelectButtonBox selectButtonBox;

    public override void Close()
    {
        base.Close();
        selectButtonBox.Close();
    }

    public void Show(List<Spell> spellDatas)
    {
        foreach (var spell in spellDatas)
        {
            Button _button = Instantiate(button);
            _button.GetComponentInChildren<TextMeshProUGUI>().text = spell.name;
            _button.onClick.AddListener(() => OnClick(spell));
            selectButtonBox.AddRegister(_button);
        }

        selectButtonBox.Show();
    }

    public void OnClick(Spell spellData)
    {
        SpellCommand spell = new SpellCommand(spellData);
        BattleInputController.instance.SelectCommand(spell);
    }
}
