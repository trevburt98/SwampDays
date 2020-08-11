using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingIngredient1 : MonoBehaviour, IItem, ICraftingIngredient
{
    private bool _equippable = false;
    public bool Equippable
    {
        get => _equippable;
    }

    private string _itemId = "craftIngredient1Ex";
    public string ID
    {
        get => _itemId;
    }

    private string _itemName = "Example Crafting Ingredient 1";
    public string Name
    {
        get => _itemName;
    }

    private string _interactPrompt = "Pick up example crafting ingredient 1";
    public string InteractPrompt
    {
        get => _interactPrompt;
        set => _interactPrompt = value;
    }

    private string _flavourText = "Example implementation of a crafting ingredient. Used to create a workflow for future ingredients";
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

    private List<int> _tags = new List<int>() { 12 };
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

    private int _craftingId = 0;
    public int CraftingID
    {
        get => _craftingId;
    }

    public void Interact(GameObject user)
    {
        ICharacter<float> character = user.GetComponent<ICharacter<float>>();
        character.Bag.GetComponent<IBag>().Add(transform.gameObject, user);
    }
}
