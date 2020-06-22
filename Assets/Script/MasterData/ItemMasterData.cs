using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemMasterData : ScriptableObject, IMasterData<Item>
{
    [SerializeField] private List<Item> items = new List<Item>();

    public List<Item> Get()
    {
        return items;
    }
}