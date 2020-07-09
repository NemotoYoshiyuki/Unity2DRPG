using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class BattleStaus
{
    private Status baseStatus;
    [SerializeField] private Status status;
    public Status Status => status;
    public Equip equip;//装備情報
    public List<Buff> buffs = new List<Buff>();
    public StatusEffect statusEffect;//状態異常

    public BattleStaus(Status status)
    {
        this.baseStatus = status;
        this.status = baseStatus.Copy();
    }



    public int Attack()
    {
        //基礎攻撃力+装備補正+強化補正
        return 0;
    }

    public void StatusUpdate()
    {
        int hp = status.hp;
        int mp = status.mp;

        //HPとMPは引き継ぐ
        status = GetBasicStatus();
        status.hp = hp;
        status.mp = mp;

        装備補正値加算();
        強化補正値加算();
    }

    public void BuffUpdate()
    {
        if (buffs == null || buffs.Count == 0) return;
        buffs.ForEach(x => x.count--);
        buffs = buffs.Where(x => x.count > 0).ToList();
    }

    private Status GetBasicStatus()
    {
        return baseStatus.Copy();
    }

    private void 装備補正値加算()
    {
        if (equip == null)
        {
            return;
        }
        Debug.Log(equip);
        Debug.Log(equip.GetMaxHp());
        status.maxHp += equip.GetMaxHp();
        status.maxMp += equip.GetMaxMp();
        status.attack += equip.GetAttack();
        status.deffence += equip.GetDeffence();
        status.speed += equip.GetSpeed();
    }

    private Status Get強化補正値()
    {
        var status = new Status();
        foreach (var item in buffs)
        {
            StatusType statusType = item.statusType;
            int value = item.value;

            switch (statusType)
            {
                case StatusType.最大HP:
                    status.maxHp += value;
                    break;
                case StatusType.最大MP:
                    status.maxMp += value;
                    break;
                case StatusType.攻撃:
                    status.attack += value;
                    break;
                case StatusType.守備:
                    status.deffence += value;
                    break;
                case StatusType.速さ:
                    status.speed += value;
                    break;
                default:
                    break;
            }
        }
        return status;
    }

    private void 強化補正値加算()
    {
        //一番大きな値を採用する　乗算
        Status 強化補正値 = Get強化補正値();
        Debug.Log(強化補正値.maxHp);
        status.maxHp += 強化補正値.maxHp;
        status.maxMp += 強化補正値.maxMp;
        status.attack += 強化補正値.attack;
        status.deffence += 強化補正値.deffence;
        status.speed += 強化補正値.speed;
    }

    public void DeleteBuff()
    {
        buffs.Clear();
    }
}
