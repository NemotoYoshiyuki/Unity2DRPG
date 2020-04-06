using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnemyMasterData : ScriptableObject
{
    public List<EnemyData> characterData = new List<EnemyData>();
}
