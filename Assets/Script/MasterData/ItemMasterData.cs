using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemMasterData : ScriptableObject
{
    public List<ItemData> items = new List<ItemData>();
}
