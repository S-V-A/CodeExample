using UnityEngine;

[System.Serializable]
public class Resource : Collectable
{
    [SerializeField]
    int _price;
    public int Price { get { return _price; } }

    public void SetData(string name, int price)
    {
        SetName(name);
        _price = price;        
    }
}