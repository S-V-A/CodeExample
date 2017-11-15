using UnityEngine;

//base class for all world items
[System.Serializable]
public abstract class Item : MonoBehaviour, ISerializable
{
    [SerializeField]
    protected string _name;

    protected void SetName(string name)
    {
        _name = name;
        gameObject.name = name;
    }    

    public void SetData(string name)
    {
        SetName(name);
    }

    public string ToSerialized()
    {
        return this.GetType().ToString() + "|" + JsonUtility.ToJson(this);
    }

    public void FromSerialized(string serialized)
    {
        JsonUtility.FromJsonOverwrite(serialized, this);
        gameObject.name = _name;
    }

    public void Store()
    {
        Storage.Instance.Add(this);
    }
}