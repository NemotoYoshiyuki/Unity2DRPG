using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemMasterData : ScriptableObject, IMasterData<ItemData>
{
    [SerializeField] private List<ItemData> items = new List<ItemData>();

    public List<ItemData> Get()
    {
        return items;
    }
}