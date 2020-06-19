using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{

    [SerializeField] private string characterName;
    [SerializeField] private int characterID;
    [Header("初期ステータス"),SerializeField] private Status status;
    [Header("覚える呪文"),SerializeField] private List<SpellData> spellDatas;
    [Header("覚えるスキル"),SerializeField] private List<SkillData> skillDatas;
    //初期装備
    [Header("初期装備"),SerializeField] private Equip equip;

    public string CharacterName => characterName;
    public int CharacterID => characterID;
    public Status Status => status;
    public List<SpellData> SpellDatas => spellDatas;
    public List<SkillData> SkillDatas => skillDatas;
    public Equip Equip => equip;
}
