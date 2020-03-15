using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string characterName;
    public int characterID;
    public Status status;
    public List<SpellData> spellDatas;
    public List<SkillData> skillDatas;
}
