using UnityEngine;
using System;
using System.Collections.Generic;

//named contsants are a lot more fail-safe and useful
class SaveKeys
{
    public static string FirstStart = "FS";
    public static string Storage = "Storage";
    public static string Inventory = "Equipment";
    public static string Collection = "Collection";
}

public class SaveSystem : MonoBehaviour
{
    [SerializeField]
    Player player;

    void Start()
    {
        if (PlayerPrefs.GetInt(SaveKeys.FirstStart, 0) == 0)
            SpawnForTesting();
        else
            RestoreAll();
    }
    
    void SpawnForTesting()
    {        
        PlayerPrefs.SetInt(SaveKeys.FirstStart, 1);
        for (int i = 0; i < 3; i++)
        {
            GameObject newGO = new GameObject();
            newGO.AddComponent<Common>();
            newGO.GetComponent<Common>().SetData("Common_" + i);
            Storage.Instance.Add(newGO.GetComponent<Common>());
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject newGO = new GameObject();
            newGO.AddComponent<Resource>();
            newGO.GetComponent<Resource>().SetData("Resource_" + i, 100 * i);
            Storage.Instance.Add(newGO.GetComponent<Resource>());
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject newGO = new GameObject();
            newGO.AddComponent<Weapon>();
            newGO.GetComponent<Weapon>().SetData("Weapon_" + i, 50 * i);
            Storage.Instance.Add(newGO.GetComponent<Weapon>());
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject newGO = new GameObject();
            newGO.AddComponent<Armor>();
            newGO.GetComponent<Armor>().SetData("Armor_" + i, 25 * i);
            Storage.Instance.Add(newGO.GetComponent<Armor>());
        }
        SaveAll();
        EventManager.TriggerEvent(EventType.LoadUI);
    }

    void OnEnable()
    {
        EventManager.StartListening(EventType.Collect, SaveCollection);
        EventManager.StartListening(EventType.Collect, SaveStorage);
        EventManager.StartListening(EventType.Equip, SaveInventory);
        EventManager.StartListening(EventType.Equip, SaveStorage);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventType.Collect, SaveCollection);
        EventManager.StopListening(EventType.Collect, SaveStorage);
        EventManager.StopListening(EventType.Equip, SaveInventory);
        EventManager.StopListening(EventType.Equip, SaveStorage);
    }

    void OnApplicationQuit()
    {
        SaveAll();
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveAll();
    }

    void SaveInventory()
    {
        string result = string.Empty;
        foreach (var item in player.Inventory)
            result += item.ToSerialized() + "*";
        PlayerPrefs.SetString(SaveKeys.Inventory, result);
    }

    void SaveCollection()
    {
        string result = string.Empty;
        foreach (var item in player.Collection)
            result += item.ToSerialized() + "*";
        PlayerPrefs.SetString(SaveKeys.Collection, result);
    }

    void SaveStorage()
    {
        string result = string.Empty;
        foreach (var item in Storage.Instance.Items)
            result += item.ToSerialized() + "*";
        PlayerPrefs.SetString(SaveKeys.Storage, result);
    }

    void SaveAll()
    {
        SaveStorage();
        SaveInventory();
        SaveCollection();
        PlayerPrefs.Save();
    }

    void RestoreInventory()
    {
        string inventoryData = PlayerPrefs.GetString(SaveKeys.Inventory, string.Empty);
        if (inventoryData != string.Empty)
            foreach (var equipment in ParseDataAndSpawn(inventoryData))
                equipment.SendMessage("Equip", player);
    }

    void RestoreCollection()
    {
        string collectionData = PlayerPrefs.GetString(SaveKeys.Collection, string.Empty);
        if (collectionData != string.Empty)
            foreach (var equipment in ParseDataAndSpawn(collectionData))
                equipment.SendMessage("Collect", player);
    }

    void RestoreStorage()
    {
        string storageData = PlayerPrefs.GetString(SaveKeys.Storage, string.Empty);
        if (storageData != string.Empty)
            foreach (var equipment in ParseDataAndSpawn(storageData))
                equipment.SendMessage("Store");
    }

    void RestoreAll()
    {        
        RestoreStorage();
        RestoreInventory();
        RestoreCollection();
        EventManager.TriggerEvent(EventType.LoadUI);
    }

    //parse string with objects data and spawn GameObjects and add desired components
    //return them for further processing
    List<GameObject> ParseDataAndSpawn(string data)
    {
        List<GameObject> tempCollection = new List<GameObject>();
        var parsed = data.Split('*');        
        foreach (var elem in parsed)
        {
            if (elem.Length < 1)
                continue;

            var descr = elem.Split('|');
            GameObject newGO = new GameObject();
            Type typ = Type.GetType(descr[0]);
            newGO.AddComponent(typ);
            newGO.GetComponent(typ).SendMessage("FromSerialized", descr[1]);
            tempCollection.Add(newGO);
        }
        return tempCollection;
    }
}