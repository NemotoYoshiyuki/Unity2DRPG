using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Effect
{
  
    public virtual void Use(BattleCharacter owner, BattleCharacter target)
    {

    }
}
