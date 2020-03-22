using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CharacterMasterData : ScriptableObject
{
    public List<PlayerData> characterData = new List<PlayerData>();
}
