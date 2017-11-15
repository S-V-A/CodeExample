using UnityEngine;
using System;
using System.Collections.Generic;

//here comes our hero
public class Player : MonoBehaviour
{
    [SerializeField]
    List<Equipable> _inventory = new List<Equipable>();
    [SerializeField]
    List<Collectable> _collection = new List<Collectable>();

    public List<Equipable> Inventory { get { return _inventory; } }
    public List<Collectable> Collection { get { return _collection; } }

    public void Equip(Equipable newItem)
    {
        _inventory.Add(newItem);
    }

    public void Drop(Equipable item)
    {
        _inventory.Remove(item);
    }

    public void Collect(Collectable newItem)
    {
        _collection.Add(newItem);
    }

    public void Drop(Collectable item)
    {
        _collection.Remove(item);
    }
}