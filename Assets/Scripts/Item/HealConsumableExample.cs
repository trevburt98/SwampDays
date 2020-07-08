using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealConsumableExample : MonoBehaviour, IConsumable
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

    private Sprite _itemImage;
    public Sprite ItemImage
    {
        get => _itemImage;
        set => _itemImage = value;
    }

    public void use(ICharacter<float> user)
    {
        user.Heal(10);
    }
}
