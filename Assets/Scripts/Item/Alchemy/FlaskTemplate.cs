using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskTemplate : MonoBehaviour, IConsumable
{
    private bool _equippable = false;
    public bool Equippable
    {
        get => _equippable;
    }

    private string _itemId = "flaskTemplate";
    public string ID
    {
        get => _itemId;
    }

    private string _itemName = "Flask";
    public string Name
    {
        get => _itemName;
        set => _itemName = value;
    }

    private string _flavourText = "Example implementation of a crafted alchemy item. Used to create a workflow for future alchemy craftables";
    public string FlavourText
    {
        get => _flavourText;
        set => _flavourText = value;
    }

    private float _weight = 2f;
    public float Weight
    {
        get => _weight;
        set => _weight = value;
    }

    private int _value = 5;
    public int MonetaryValue
    {
        get => _value;
        set => _value = value;
    }

    private int _spaces = 1;
    public int InventorySpaces
    {
        get => _spaces;
        set => _spaces = value;
    }
    private List<int> _tags = new List<int>() { 4, 11 };
    public List<int> Tags
    {
        get => _tags;
    }

    private Sprite _itemImage;
    public Sprite ItemImage
    {
        get => _itemImage;
        set => _itemImage = value;
    }

    private int _maxStack = 1;
    public int MaxStack
    {
        get => _maxStack;
    }

    private int _currentStack = 1;
    public int NumInStack
    {
        get => _currentStack;
        set => _currentStack = value;
    }

    private int healthChangeAmount;

    public void Interact(GameObject user)
    {
        ICharacter<float> character = user.GetComponent<ICharacter<float>>();
        character.Bag.GetComponent<IBag>().Add(transform.gameObject, user);
    }

    public void SetFlaskValues(int healthChange)
    {
        healthChangeAmount = healthChange;
    }

    public void Use(ICharacter<float> user)
    {
        user.ChangeCurrentHealth(healthChangeAmount);
    }
}
