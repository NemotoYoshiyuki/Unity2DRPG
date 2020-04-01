using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{

    [SerializeField] private string characterName;
    [SerializeField] private int characterID;
    [SerializeField] private Status status;
    [SerializeField] private List<SpellData> spellDatas;
    [SerializeField] private List<SkillData> skillDatas;

    public string CharacterName => characterName;
    public int CharacterID => characterID;
    public Status Status => status;
    public List<SpellData> SpellDatas => spellDatas;
    public List<SkillData> SkillDatas => skillDatas;
}
