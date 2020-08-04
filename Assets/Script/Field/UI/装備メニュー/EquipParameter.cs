using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipParameter : MonoBehaviour
{
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI mp;
    public TextMeshProUGUI attack;
    public TextMeshProUGUI defence;
    public TextMeshProUGUI speed;

    public void ShowParameter(CharacterData character)
    {
        characterName.text = character.GetName();
        Status status = character.GetStatus();
        hp.text = status.hp.ToString();
        mp.text = status.mp.ToString();
        attack.text = status.attack.ToString();
        defence.text = status.deffence.ToString();
        speed.text = status.speed.ToString();
    }

    public void ShowParameter(CharacterData character, Equip equip)
    {
        Equip currentEquip = character.equip;
        Equip newEquip = equip;

        Status status = character.GetStatus();
        characterName.text = character.GetName();
        hp.text = status.hp.ToString() + GetParameterText(newEquip.GetMaxHp() - currentEquip.GetMaxHp());
        mp.text = status.mp.ToString() + GetParameterText(newEquip.GetMaxMp() - currentEquip.GetMaxMp());
        attack.text = status.attack.ToString() + GetParameterText(newEquip.GetAttack() - currentEquip.GetAttack());
        defence.text = status.deffence.ToString() + GetParameterText(newEquip.GetDeffence() - currentEquip.GetDeffence());
        speed.text = status.speed.ToString() + GetParameterText(newEquip.GetSpeed() - currentEquip.GetSpeed());
    }

    public void ShowParameter(CharacterData character, Weapon weapon)
    {
        //武器を装備した場合のパラメータ
        Equip equip = character.equip.Copy();
        equip.weapon = weapon;
        ShowParameter(character, equip);
    }

    public void ShowParameter(CharacterData character, Armor armor)
    {
        //防具を装備した場合のパラメータ
        Equip equip = character.equip.Copy();
        equip.armor = armor;
        ShowParameter(character, equip);
    }

    public void ShowParameter(CharacterData character, Accessory accessory)
    {
        //装飾を装備した場合のパラメータ
        Equip equip = character.equip.Copy();
        equip.accessory = accessory;
        ShowParameter(character, equip);
    }

    public string GetParameterText(int value)
    {
        string m_text = string.Empty;
        if (value == 0) m_text += string.Empty;
        else if (value > 0) m_text += $"　<color=green><voffset=0.2em>+</voffset>{value}</color>";
        else if (value < 0) m_text += $"　<color=red><voffset=0.2em></voffset>{value}</color>";
        return m_text;
    }
}
