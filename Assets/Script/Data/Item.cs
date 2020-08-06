using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Item : Command
{
    public int id;
    public string itemName;
    public UseType useType;
    public TargetUnit targetUnit;
    public TargetRange targetRange;
    public int mp;
    public string description;
    public int price;

    //スキルの効果
    [SerializeReference, SubclassSelector] public List<Effect> effects;

    public override BattleCommand CreateBattleCommand()
    {
        return new ItemCommand(this);
    }
}
