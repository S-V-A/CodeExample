using UnityEngine;
using UnityEngine.UI;

public class CommonUI : MonoBehaviour
{
    [SerializeField]
    protected Text _classText;
    [SerializeField]
    protected Text _nameText;

    public void SetItem(Item item)
    {
        SetContent(item);
    }

    protected void SetContent(Item item)
    {
        _nameText.text = item.name;
        _classText.text = item.GetType().ToString();
    }
}