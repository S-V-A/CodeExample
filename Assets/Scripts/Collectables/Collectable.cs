using UnityEngine;

public abstract class Collectable : Item, ICollectable
{
    public void Collect(Player player)
    {        
        player.Collect(this);
        Storage.Instance.Remove(this);
        EventManager.TriggerEvent(EventType.Collect);
    }
    public void Drop()
    {
        Store();
        EventManager.TriggerEvent(EventType.Collect);
    }
}