using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class BattleStaus
{
    private Status baseStatus;
    public Status status;
    //装備情報
    public List<Buff> buffs = new List<Buff>();

    public BattleStaus(Status status)
    {
        this.baseStatus = status;
        this.status = baseStatus.Copy();
    }

    public void Update()
    {
        //ステータスの更新
        status = baseStatus.Copy();
        //UpdateBuff();
        ReCalculation();
    }

    public void ReCalculation()
    {
        foreach (var item in buffs)
        {
            StatusType statusType = item.statusType;
            int value = item.value;

            switch (statusType)
            {
                case StatusType.最大HP:
                    break;
                case StatusType.現在HP:
                    break;
                case StatusType.最大MP:
                    break;
                case StatusType.現在MP:
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
                case StatusType.全て:
                    break;
                default:
                    break;
            }
        }
    }

    public void UpdateBuff()
    {
        if (buffs == null || buffs.Count == 0) return;
        buffs.ForEach(x => x.count--); ;
        buffs = buffs.Where(x => x.count <= 1).ToList();
    }

    public void DeleteBuff()
    {
        buffs.Clear();
    }
}
