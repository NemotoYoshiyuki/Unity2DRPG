using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class InventorySystem : MonoBehaviour
{
    [SerializeField] private ItemMasterData itemMasterData;
    [SerializeField] private List<Item> itemDatas = new List<Item>();
    [SerializeField] private List<Equipment> equipments = new List<Equipment>();

    protected static InventorySystem instance;
    public static InventorySystem Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<InventorySystem>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }

    protected static InventorySystem Create()
    {
        GameObject inventorySystem = new GameObject("InventorySystem");
        instance = inventorySystem.AddComponent<InventorySystem>();
        Initialize();

        return instance;
    }

    protected static void Initialize()
    {

    }

    public static void Initialize(List<Item> items)
    {
        Instance.itemDatas = new List<Item>(items);
    }

    private void Awake()
    {
        //シングルトン
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static List<Item> GetItems()
    {
        return Instance.itemDatas;
    }

    public static List<Equipment> GetEquipments()
    {
        return Instance.equipments;
    }

    public static void AddItem(Item itemData)
    {
        Instance.itemDatas.Add(itemData);
    }

    public static void AddEqip(Equipment equipment)
    {
        Instance.equipments.Add(equipment);
    }

    public static void AddItem(int id)
    {
        Item itemData = Instance.itemMasterData.Get().FirstOrDefault(x => x.id == id);
        Instance.itemDatas.Add(itemData);
    }

    public static void UseItem(Item itemData)
    {
        Instance.itemDatas.Remove(itemData);
    }

    public static void UseItem(int id)
    {
        Item itemData = Instance.itemMasterData.Get().FirstOrDefault(x => x.id == id);
        Instance.itemDatas.Remove(itemData);
    }

    public static bool HasItem(int id)
    {
        return Instance.itemDatas.Any(x => x.id == id);
    }

    public static void Save()
    {
        SaveData.InventoryData data = SaveSystem.saveData.inventoryData;
        data.items = new List<Item>(Instance.itemDatas);
        data.equipment = new List<Equipment>(Instance.equipments);
    }

    public static void Load()
    {
        SaveData.InventoryData data = SaveSystem.saveData.inventoryData;
        Instance.itemDatas = new List<Item>(data.items);
        Instance.equipments = new List<Equipment>(data.equipment);
    }
}
