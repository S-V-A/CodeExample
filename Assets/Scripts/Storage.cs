using UnityEngine;
using System.Collections.Generic;

//Singleton is often called an anti-pattern, however it is iseful for global storages like this
//Anyway, must be handled with care
public class Storage : Singleton<Storage>
{
    [SerializeField]
    List<Item> _items = new List<Item>();
    public List<Item> Items { get { return _items; } }

    public void Add(Item item)
    {
        _items.Add(item);
    }

    public void Remove(Item item)
    {
        _items.Remove(item);
    }
}