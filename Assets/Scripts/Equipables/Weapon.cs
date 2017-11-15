using UnityEngine;

public class Weapon : Equipable
{
    [SerializeField]
    int _damage;
    public int Damage { get { return _damage; } }

    public void SetData(string name, int damage)
    {
        SetName(name);
        _damage = damage;        
    }
}