interface ISerializable
{
    string ToSerialized();
    void FromSerialized(string serialized);
}