using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{

    [SerializeField] private string characterName;
    [SerializeField] private int characterID;
    [Header("初期ステータス"), SerializeField] private Status status;
    [Header("成長率"), SerializeField] private Status growthRate;
    [Header("覚える呪文"), SerializeField] private List<Spell> spellDatas;
    [Header("覚えるスキル"), SerializeField] private List<Skill> skillDatas;
    [Header("初期装備"), SerializeField] private Equip equip;

    public string CharacterName => characterName;
    public int CharacterID => characterID;
    public Status Status => status;
    public Status GrowthRate => growthRate;
    public List<Spell> SpellDatas => spellDatas;
    public List<Skill> SkillDatas => skillDatas;
    public Equip Equip => equip;
}
