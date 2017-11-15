using UnityEngine;

public class Armor : Equipable
{
    [SerializeField]
    int _protection;
    public int Protection { get { return _protection; } }

    public void SetData(string name, int protection)
    {
        SetName(name);
        _protection = protection;        
    }
}