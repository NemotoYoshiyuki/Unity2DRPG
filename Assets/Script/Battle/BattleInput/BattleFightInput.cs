using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFightInput : BattleCommandInput
{
    public void Attack()
    {
        BattleInputController.instance.SelectCommand(new AttackCommand());
    }

    public void Defencce()
    {
        BattleInputController.instance.SelectCommand(new DefenseCommand());
    }

    public void Spell()
    {
        BattleInputController.instance.SpellWindow();
    }

    public void Skill()
    {
        BattleInputController.instance.SkillInputWindow();
    }

    public void Item()
    {
        BattleInputController.instance.OpenItemInputWindow();
    }
}
