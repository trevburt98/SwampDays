using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupThatIsTooHot : MonoBehaviour, IConsumable
{
    private bool _equippable = false;
    public bool Equippable
    {
        get => _equippable;
    }

    private string _itemId = "soupTooHot";
    public string ID
    {
        get => _itemId;
    }

    private string _itemName = "Soup That Is Too Hot";
    public string Name
    {
        get => _itemName;
    }

    private string _interactPrompt = "Pick up that hot ass soup";
    public string InteractPrompt
    {
        get => _interactPrompt;
        set => _interactPrompt = value;
    }

    private string _flavourText = "This is a bowl of soup that is too hot to be eaten safely. Conveniently for the purpose of testing items with multiple tags on them, that means that this is simultaneously a food, drink, healing, and harmful item.";
    public string FlavourText
    {
        get => _flavourText;
        set => _flavourText = value;
    }

    private int _spaces = 1;
    public int InventorySpaces{
        get => _spaces;
        set => _spaces = value;
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

    private List<int> _tags = new List<int>(){4, 5, 6, 7};
    public List<int> Tags{
        get => _tags;
    }

    private Sprite _itemImage;
    public Sprite ItemImage
    {
        get => _itemImage;
        set => _itemImage = value;
    }

    public void Interact(GameObject user)
    {
        ICharacter<float> character = user.GetComponent<ICharacter<float>>();
        character.Bag.GetComponent<IBag>().Add(transform.gameObject, user);
    }

    public void Use(ICharacter<float> user)
    {
        user.Heal(10);
        user.Damage(5);
    }
}
