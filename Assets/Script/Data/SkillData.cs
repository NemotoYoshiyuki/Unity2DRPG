using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Command/SkillData")]
public class SkillData : Command
{
    public int id;
    public string skillName;
    public UseType useType;
    public TargetUnit targetUnit;
    public TargetRange targetRange;
    public int mp;
    public int hitRate = 100;
    public string description;

    //スキルの効果
    [SerializeReference, SubclassSelector] public List<CommandEffect> effects;
   
}
