using Character.PlayerCharacter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealConsumableExample : MonoBehaviour, IConsumable, IAlchemyIngredient
{
    private bool _equippable = false;
    public bool Equippable
    {
        get => _equippable;
    }

    private string _itemId = "consHealEx";
    public string ID
    {
        get => _itemId;
    }

    private string _itemName = "Example Healing Consumable";
    public string Name
    {
        get => _itemName;
    }

    private string _interactPrompt = "Pick up example healing consumable";
    public string InteractPrompt
    {
        get => _interactPrompt;
        set => _interactPrompt = value;
    }

    private string _flavourText = "Example implementation of a consumable that heals the player. Used to create a workflow for future consumables";
    public string FlavourText
    {
        get => _flavourText;
        set => _flavourText = value;
    }

    private float _weight = 15.6f;
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
    private List<int> _tags = new List<int>(){3, 4};
    public List<int> Tags{
        get => _tags;
    }

    private Sprite _itemImage;
    public Sprite ItemImage
    {
        get => _itemImage;
        set => _itemImage = value;
    }

    private int _maxStack = 10;
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

    private int _optTemp = 375;
    public int OptimalTemp
    {
        get => _optTemp;
    }

    private int _minTemp = 150;
    public int MinTemp
    {
        get => _minTemp;
    }

    private int _maxTemp = 400;
    public int MaxTemp
    {
        get => _maxTemp;
    }

    public void Interact(GameObject user)
    {
        ICharacter<float> character = user.GetComponent<ICharacter<float>>();
        character.Bag.GetComponent<IBag>().Add(transform.gameObject, user);
    }

    public void Use(ICharacter<float> user)
    {
        user.Heal(10);
    }
}
