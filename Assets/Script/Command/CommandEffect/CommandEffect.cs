using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class CommandEffect
{
  
    public virtual void Use(BattleCharacter owner, BattleCharacter target)
    {

    }
}
