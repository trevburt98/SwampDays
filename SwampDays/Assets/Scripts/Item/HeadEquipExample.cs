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

    private string _id = "exHeadEquip";
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

    private int _equipSlot = 1;
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

    private Image _image;
    public Image EquipmentImage
    {
        get => _image;
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
