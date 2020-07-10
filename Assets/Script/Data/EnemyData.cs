using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string characterName;
    [SerializeField] private int id;
    [SerializeField] private Sprite graphic;
    [SerializeField] private Status status;
    [Header("戦利品")]
    [SerializeField] private int dropGold;
    [SerializeField] private float dropRate;
    [SerializeField] private Item dropItem;
    [Header("戦闘中使用する技"), SerializeField] private List<Command> commands;

    public string CharacterName => characterName;
    public int Id => id;
    public Sprite Graphic => graphic;
    public Status Status => status;
    public int DropGold => dropGold;
    public float DropRate => dropRate;
    public Item DropItem => dropItem;
    public List<Command> Commands => commands;
}
