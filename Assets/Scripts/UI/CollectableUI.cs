using UnityEngine;
using UnityEngine.UI;

public class CollectableUI : CommonUI
{
    Collectable _item;
    bool _collected = false;
    TestUI _mainUI;
    [SerializeField]
    Text _propertyText;
    [SerializeField]
    Text _buttonText;

    public void SetResource(Resource item, bool collected, TestUI mainUI)
    {
        SetContent(item);
        SetState(collected);
        _item = item;
        _mainUI = mainUI;
        _propertyText.text = "Price = " + item.Price.ToString();
        _collected = collected;
    }

    void SetState(bool state)
    {
        _collected = state;
        if (_collected)
            _buttonText.text = "Remove";
        else
            _buttonText.text = "Add";
    }    

    public void OnClick()
    {
        if (_collected)
        {
            _mainUI.DropByPlayer(_item, this);
            _buttonText.text = "Add";
            _collected = false;
        }
        else
        {
            _mainUI.CollectByPlayer(_item, this);
            _buttonText.text = "Remove";
            _collected = true;
        }
    }
}