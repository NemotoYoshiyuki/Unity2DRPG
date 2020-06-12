using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip
{
    public Weapon weapon;
    public Armor armor;
    public Accessory accessory;

    public void SetWeapon()
    {

    }
    public void SetArmor()
    {

    }
    public void SetAccessory()
    {

    }

    public int GetMaxHp()
    {
        int weaponValue = weapon?.maxHp ?? 0;
        int armorValue = armor?.maxHp ?? 0;
        int accessoryValue = accessory?.maxHp ?? 0;

        return weaponValue + armorValue + accessoryValue;
    }

    public int GetMaxMp()
    {
        int weaponValue = weapon?.maxMp ?? 0;
        int armorValue = armor?.maxMp ?? 0;
        int accessoryValue = accessory?.maxMp ?? 0;

        return weaponValue + armorValue + accessoryValue;
    }

    public int GetAttack()
    {
        int weaponValue = weapon?.attack ?? 0;
        int armorValue = armor?.attack ?? 0;
        int accessoryValue = accessory?.attack ?? 0;

        return weaponValue + armorValue + accessoryValue;
    }

    public int GetDeffence()
    {
        int weaponValue = weapon?.deffence ?? 0;
        int armorValue = armor?.deffence ?? 0;
        int accessoryValue = accessory?.deffence ?? 0;

        return weaponValue + armorValue + accessoryValue;
    }

    public int GetSpeed()
    {
        int weaponValue = weapon?.speed ?? 0;
        int armorValue = armor?.speed ?? 0;
        int accessoryValue = accessory?.speed ?? 0;

        return weaponValue + armorValue + accessoryValue;
    }
}
