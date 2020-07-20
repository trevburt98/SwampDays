using Character.PlayerCharacter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmConsumableExample : MonoBehaviour, IConsumable
{
    private bool _equippable = false;
    public bool Equippable
    {
        get => _equippable;
    }

    private string _itemId = "consHarmEx";
    public string ID
    {
        get => _itemId;
    }

    private string _itemName = "Example Harmful Consumable";
    public string Name
    {
        get => _itemName;
    }

    private string _flavourText = "Example implementation of a consumable that harms the player. Used to create a workflow for future consumables";
    public string FlavourText
    {
        get => _flavourText;
        set => _flavourText = value;
    }

    private float _weight = 22.89f;
    public float Weight
    {
        get => _weight;
        set => _weight = value;
    }

    private int _value = 10;
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

    private List<int> _tags = new List<int>(){5};
    public List<int> Tags{
        get => _tags;
    }
    
    private Sprite _itemImage;
    public Sprite ItemImage
    {
        get => _itemImage;
        set => _itemImage = value;
    }

    private int _maxStack = 2;
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

    public void Use(ICharacter<float> user)
    {
        user.Damage(10);
    }
}
