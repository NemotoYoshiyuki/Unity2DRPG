using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSkillInput : BattleCommandInput
{
    public Button button;
    public SelectButtonBox selectButtonBox;

    public override void Close()
    {
        base.Close();
        selectButtonBox.Close();
    }

    public void Show(List<Skill> skillDatas)
    {
        foreach (var skill in skillDatas)
        {
            Button _button = Instantiate(button);
            _button.GetComponentInChildren<TextMeshProUGUI>().text = skill.name;
            _button.onClick.AddListener(() => OnClick(skill));
            selectButtonBox.AddRegister(_button);
        }

        selectButtonBox.Show();
    }

    public void OnClick(Skill skillData)
    {
        SkillCommand skill = new SkillCommand(skillData);
        BattleInputController.instance.SelectCommand(skill);
    }

}
