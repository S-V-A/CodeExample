using UnityEngine;
using System.Collections.Generic;

//main class for UI
public class TestUI : MonoBehaviour
{
    [SerializeField]
    GameObject equipableUIPrefab;
    [SerializeField]
    GameObject collectableUIPrefab;
    [SerializeField]
    GameObject commonUIPrefab;
    [SerializeField]
    Player player;
    [SerializeField]
    Transform storageContainer;
    [SerializeField]
    Transform inventoryContainer;
    [SerializeField]
    Transform collectionContainer;

    void OnEnable()
    {
        EventManager.StartListening(EventType.LoadUI, LoadUI);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventType.LoadUI, LoadUI);
    }

    void LoadUI()
    {
        SpawnStorage();
        SpawnEquipment();
        SpawnCollection();
    }

    void SpawnEquipment()
    {
        foreach (Equipable equip in player.Inventory)
            SpawnEquipable(equip, inventoryContainer, true);
    }

    void SpawnEquipable(Equipable equip, Transform parent, bool equipped)
    {
        if (equip is Weapon)
        {
            Weapon weapon = equip as Weapon;
            GameObject invGO = Instantiate(equipableUIPrefab, parent);
            invGO.GetComponent<EquipableUI>().SetWeapon(weapon, equipped, this);
        }
        else if (equip is Armor)
        {
            Armor armor = equip as Armor;
            GameObject invGO = Instantiate(equipableUIPrefab, parent);
            invGO.GetComponent<EquipableUI>().SetArmor(armor, equipped, this);
        }
    }

    void SpawnCollection()
    {
        foreach (Collectable coll in player.Collection)
            SpawnCollectable(coll, collectionContainer, true);
    }

    void SpawnCollectable(Collectable coll, Transform parent, bool collected)
    {
        if (coll is Resource)
        {
            Resource res = coll as Resource;
            GameObject resGO = Instantiate(collectableUIPrefab, parent);
            resGO.GetComponent<CollectableUI>().SetResource(res, collected, this);
        }
    }

    void SpawnStorage()
    {
        foreach (Item item in Storage.Instance.Items)
        {
            if (item is Common)
            {
                GameObject itemGO = Instantiate(commonUIPrefab, storageContainer);
                itemGO.GetComponent<CommonUI>().SetItem(item);
            }
            else if (item is Collectable)
                SpawnCollectable(item as Collectable, storageContainer, false);
            else if (item is Equipable)
                SpawnEquipable(item as Equipable, storageContainer, false);
        }
    }

    public void EquipByPlayer(Equipable equipment, EquipableUI element)
    {
        equipment.Equip(player);
        element.transform.SetParent(inventoryContainer);
    }

    public void DropByPlayer(Equipable equipment, EquipableUI element)
    {
        equipment.Drop();
        player.Drop(equipment);
        element.transform.SetParent(storageContainer);
    }

    public void CollectByPlayer(Collectable collectable, CollectableUI element)
    {
        collectable.Collect(player);
        element.transform.SetParent(collectionContainer);
    }

    public void DropByPlayer(Collectable collectable, CollectableUI element)
    {
        collectable.Drop();
        player.Drop(collectable);
        element.transform.SetParent(storageContainer);
    }
}