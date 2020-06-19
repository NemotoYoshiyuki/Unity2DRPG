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
    [SerializeField] private List<Command> commands;

    public string CharacterName => characterName;
    public int Id => id;
    public Sprite Graphic => graphic;
    public Status Status => status;
    public List<Command> Commands => commands;
}
