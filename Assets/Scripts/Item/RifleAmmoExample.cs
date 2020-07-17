using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAmmoExample : MonoBehaviour, IAmmo
{
    private bool _equippable = false;
    public bool Equippable
    {
        get => _equippable;
    }

    private string _itemId = "rifleAmmoEx";
    public string ID
    {
        get => _itemId;
    }

    private string _itemName = "Example Rifle Ammo";
    public string Name
    {
        get => _itemName;
    }

    private string _flavourText = "Example implementation of ammo for the rifle. Used to create a workflow for future ammo types";
    public string FlavourText
    {
        get => _flavourText;
        set => _flavourText = value;
    }

    private float _weight = 0.1f;
    public float Weight
    {
        get => _weight;
        set => _weight = value;
    }

    private int _value = 1;
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

    private Sprite _itemImage;
    public Sprite ItemImage
    {
        get => _itemImage;
        set => _itemImage = value;
    }

    private int _maxStack = 20;
    public int MaxStack
    {
        get => _maxStack;
    }

    private int _currentStack = 15;
    public int NumInStack
    {
        get => _currentStack;
        set => _currentStack = value;
    }

    private float _damageMultiplier = 1.5f;
    public float DamageMultiplier
    {
        get => _damageMultiplier;
    }

    private int _effect;
    public int Effect
    {
        get => _effect;
    }
}
