using UnityEngine;

public abstract class Equipable: Item, IEquipable
{
    public void Equip(Player player)
    {
        player.Equip(this);
        Storage.Instance.Remove(this);
        EventManager.TriggerEvent(EventType.Equip);
    }

    public void Drop()
    {
        Store();
        EventManager.TriggerEvent(EventType.Equip);
    }
}