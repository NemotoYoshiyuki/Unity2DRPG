using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BattleCharacter
{
    public EnemyData enemyData;
    public override string CharacterName { get => enemyData.characterName; set => enemyData.characterName = value; }

    public List<Command> GetCommands()
    {
        return enemyData.commands;
    }

    public override void OnDead()
    {
        base.OnDead();
        gameObject.SetActive(false);
    }

    public int DropExp()
    {
        return enemyData.exp;
    }
}
