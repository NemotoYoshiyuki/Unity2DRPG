using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public InventorySystem inventorySystem = new InventorySystem();

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static InventorySystem GetInventorySystem()
    {
        return instance.inventorySystem;
    }
}
