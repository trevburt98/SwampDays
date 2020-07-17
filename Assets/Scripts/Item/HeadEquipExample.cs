using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadEquipExample : MonoBehaviour, IEquipment
{
    #region Interface Implementations
    private bool _equippable = true;
    public bool Equippable
    {
        get => _equippable;
    }

    private string _id = "hEquipEx";
    public string ID
    {
        get => _id;
    }

    private string _name = "Example Head Equipment";
    public string Name
    {
        get => _name;
    }

    private string _flavourText = "Example implementation of a equipment equipped on the head. Used to create a workflow for future equipments";
    public string FlavourText
    {
        get => _flavourText;
        set => _flavourText = value;
    }

    private float _weight = 5f;
    public float Weight
    {
        get => _weight;
        set => _weight = value;
    }

    private int _value = 25;
    public int MonetaryValue
    {
        get => _value;
        set => _value = value;
    }

    private int _equipSlot = 0;
    public int EquipSlot
    {
        get => _equipSlot;
    }

    private int _armourValue = 3;
    public int ArmourValue
    {
        get => _armourValue;
        set => _armourValue = value;
    }

    private int _durability = 100;
    public int Durability
    {
        get => _durability;
        set => _durability = value;
    }

    private int _spaces = 3;
    public int InventorySpaces
    {
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

    [SerializeField] private Sprite _image;
    public Sprite ItemImage
    {
        get => _image;
        set => _image = value;
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
