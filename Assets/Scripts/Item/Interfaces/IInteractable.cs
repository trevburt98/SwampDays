using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool Equippable
    {
        get;
    }

    string ID
    {
        get;
    }

    string Name
    {
        get;
    }

    string FlavourText
    {
        get;
        set;
    }

    float Weight
    {
        get;
        set;
    }

    int MonetaryValue
    {
        get;
        set;
    }

    int InventorySpaces
    {
        get;
        set;
    }

    int MaxStack
    {
        get;
    }

    int NumInStack
    {
        get;
        set;
    }

    Sprite ItemImage
    {
        get;
        set;
    }
}
