using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string characterName;
    public Status status;

    public Sprite graphic;
    public int exp;
    public List<Command> commands;
}
