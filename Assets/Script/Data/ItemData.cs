using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemData : Command
{
    public int id;
    public string itemName;
    public UseType useType;
    public TargetUnit targetUnit;
    public TargetRange targetRange;
    public int mp;
    public string description;

    //スキルの効果
    [SerializeReference, SubclassSelector] public List<CommandEffect> effects;
}
