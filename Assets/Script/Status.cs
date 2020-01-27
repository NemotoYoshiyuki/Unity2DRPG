using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Status 
{
    public int lv;
    public int maxHp;
    public int hp;
    public int maxMp;
    public int mp;
    public int attack;
    public int deffence;
    public int speed;
    public int exp;

    public Status Copy()
    {
        return (Status)this.MemberwiseClone();
    }
}
