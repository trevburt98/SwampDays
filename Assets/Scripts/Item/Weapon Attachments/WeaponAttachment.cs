using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttachment : MonoBehaviour, IItem
{
    #region Attachment Info
    //0 - Barrel
    //1 - Grip
    //2 - Magazine
    //3 - Sight
    //4 - Stock
    public int attachmentType;
    public List<WeaponModifier> modifierList;
    public List<IRangedWeapon> validWeapons;
    #endregion

    #region Item Implementation
    private bool _equippable = false;
    public bool Equippable
    {
        get => _equippable;
    }

    [SerializeField] private string _itemId;
    public string ID
    {
        get => _itemId;
    }

    [SerializeField] private string _itemName = "Example Attachment";
    public string Name
    {
        get => _itemName;
    }

    [SerializeField] private string _interactPrompt = "Pick up attachment";
    public string InteractPrompt
    {
        get => _interactPrompt;
        set => _interactPrompt = value;
    }

    [SerializeField] private string _flavourText = "Example implementation of an attachment. Used to create a workflow for future attachments";
    public string FlavourText
    {
        get => _flavourText;
        set => _flavourText = value;
    }

    [SerializeField] private float _weight = 2f;
    public float Weight
    {
        get => _weight;
        set => _weight = value;
    }

    [SerializeField] private int _value = 10;
    public int MonetaryValue
    {
        get => _value;
        set => _value = value;
    }

    [SerializeField] private int _spaces = 1;
    public int InventorySpaces
    {
        get => _spaces;
        set => _spaces = value;
    }

    [SerializeField] private List<int> _tags = new List<int>() {};
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
    #endregion

    public void Interact(GameObject user)
    {
        ICharacter<float> character = user.GetComponent<ICharacter<float>>();
        character.Bag.GetComponent<IBag>().Add(transform.gameObject, user);
    }
}
