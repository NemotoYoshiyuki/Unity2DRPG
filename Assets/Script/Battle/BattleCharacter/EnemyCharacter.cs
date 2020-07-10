using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BattleCharacter
{
    public EnemyData enemyData;
    private string characterName;
    public override string CharacterName { get => characterName; set => characterName = value; }

    public List<Command> GetCommands()
    {
        return enemyData.Commands;
    }

    public override void OnDead()
    {
        base.OnDead();
        gameObject.SetActive(false);
    }

    public int DropGold()
    {
        return enemyData.DropGold;
    }

    public int DropExp()
    {
        return enemyData.Status.exp;
    }
}
