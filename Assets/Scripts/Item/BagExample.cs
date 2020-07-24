using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private List<int> _tags = new List<int>(){10};
    public List<int> Tags{
        get => _tags;
    }

    private float _totalWeight = 2f;
    public float TotalWeight
    {
        get => _totalWeight;
        set => _totalWeight = value;
    }


    public void Interact(GameObject user)
    {
        ICharacter<float> character = user.GetComponent<ICharacter<float>>();
        if(character.Bag == null)
        {
            transform.gameObject.SetActive(false);
            transform.parent = gameObject.transform;
            character.Bag = transform.gameObject;
        } else if(character.Bag.GetComponent<IBag>().Add(transform.gameObject) && character is PlayerCharacter)
        {
            PlayerCharacter player = user.GetComponent<PlayerCharacter>();
            player.updateCarryingCapacity(Weight);
        }
    }

    public void Remove(GameObject item)
    {
        CurrentSpaces -= item.GetComponent<IItem>().InventorySpaces;
        TotalWeight -= item.GetComponent<IItem>().Weight;
        item.transform.parent = null;
        Destroy(item);
    }

    public GameObject Find(string itemID)
    {
        GameObject ret = null;
        //TODO: take into account stackables, find the one with the lowest(?) numinstack
        foreach(Transform child in gameObject.transform)
        {
            if(child.GetComponent<IItem>().ID == itemID)
            {
                ret = child.gameObject;
            }
        }
        return ret;
    }

    //Add an item to the inventory of this bag, return whether or not the addition was successful
    public bool Add(GameObject itemToAdd)
    {
        List<IItem> inventoryList = GetComponentsInChildren<IItem>().ToList();
        IItem item = itemToAdd.GetComponent<IItem>();

        bool ret = false;
        //If we have enough space in the bag to add the given item
        if(CurrentSpaces + item.InventorySpaces <= MaxSpaces)
        {
            //If the item is both stackable and we currently have that item in inventory
            if(inventoryList.Exists(x => x.ID == item.ID))
            {
                List<IItem> found = inventoryList.FindAll(x => x.ID == item.ID);
                bool added = false;
                foreach(IItem instance in found)
                {
                    if (!added && instance.NumInStack + item.NumInStack <= instance.MaxStack)
                    {
                        instance.NumInStack += item.NumInStack;
                        added = true;
                    }
                }

                if(!added)
                {
                    itemToAdd.SetActive(false);
                    itemToAdd.transform.parent = gameObject.transform;
                }
            } else
            {
                itemToAdd.SetActive(false);
                itemToAdd.transform.parent = gameObject.transform;
            }
            CurrentSpaces += item.InventorySpaces;
            TotalWeight += item.Weight;
            ret = true;
        } else
        {
            Debug.Log("too full");
        }
        return ret;
    }
}
