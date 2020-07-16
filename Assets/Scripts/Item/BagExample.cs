using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagExample : MonoBehaviour, IBag
{
    private bool _equippable = true;
    public bool Equippable
    {
        get => _equippable;
    }

    private string _id = "bagEx";
    public string ID 
    {
        get => _id;
    }

    private string _name = "Bag Example";
    public string Name
    {
        get => _name;
    }

    private string _flavourText = "An example bag, used to establish workflow for future bags";
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

    private int _value = 50;
    public int MonetaryValue
    {
        get => _value;
        set => _value = value;
    }

    private int _inventorySpaces = 2;
    public int InventorySpaces
    {
        get => _inventorySpaces;
        set => _inventorySpaces = value;
    }


    private Sprite _itemImage;
    public Sprite ItemImage
    {
        get => _itemImage;
        set => _itemImage = value;
    }

    private int _maxSpaces = 10;
    public int MaxSpaces
    {
        get => _maxSpaces;
        set => _maxSpaces = value;
    }

    private int _currentSpaces = 0;
    public int CurrentSpaces
    {
        get => _currentSpaces;
        set => _currentSpaces = value;
    }

    private List<IInteractable> _inventory;
    public List<IInteractable> Inventory
    {
        get => _inventory;
        set => _inventory = value;
    }

    void Start()
    {
        Inventory = new List<IInteractable>(MaxSpaces);
    }

    public void Remove(IInteractable item)
    {
        Inventory.Remove(item);
        CurrentSpaces -= item.InventorySpaces;
    }

    //Add an item to the inventory of this bag, return whether or not the addition was successful
    public bool Add(IInteractable item)
    {
        bool ret = false;
        //If we have enough space in the bag to add the given item
        if(CurrentSpaces + item.InventorySpaces <= MaxSpaces)
        {
            //If the item is both stackable and we currently have that item in inventory
            if(item is IStackable && Inventory.Exists(x => x.ID == item.ID))
            {
                IStackable found = Inventory.Find(x => x.ID == item.ID) as IStackable;
                Debug.Log(found.NumInStack);
                found.ChangeNumInStack(1);

            } else
            {
                Inventory.Add(item);
            }
            CurrentSpaces += item.InventorySpaces;
            ret = true;
        } else
        {
            Debug.Log("too full");
        }
        return ret;
    }
}
