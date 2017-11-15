using UnityEngine;
using UnityEngine.UI;

public class EquipableUI : CommonUI
{
    Equipable _item;
    bool _equipped = false;
    TestUI _mainUI;
    [SerializeField]
    Text _propertyText;
    [SerializeField]
    Text _buttonText;

    public void SetWeapon(Weapon item, bool equipped, TestUI mainUI)
    {
        SetContent(item);
        SetState(equipped);
        _item = item;
        _mainUI = mainUI;
        _propertyText.text = "Damage = " + item.Damage.ToString();        
    }

    public void SetArmor(Armor item, bool equipped, TestUI mainUI)
    {
        SetContent(item);
        SetState(equipped);
        _item = item;
        _mainUI = mainUI;
        _propertyText.text = "Protection = " + item.Protection.ToString();        
    }

    void SetState(bool state)
    {
        _equipped = state;
        if (_equipped)
            _buttonText.text = "Remove";
        else
            _buttonText.text = "Add";
    }

    public void OnClick()
    {
        if (_equipped)
        {
            _mainUI.DropByPlayer(_item, this);
            _buttonText.text = "Add";
            _equipped = false;
        }
        else
        {
            _mainUI.EquipByPlayer(_item, this);
            _buttonText.text = "Remove";
            _equipped = true;
        }
    }
}