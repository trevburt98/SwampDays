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

    //0 - Rifle
    //1 - Handgun
    //2 - Ammunition
    //3 - Ingredient
    //4 - Healing Item
    //5 - Harmful Item
    //6 - Food
    //7 - Drink
    //8 - Head Armour
    //9 - Body Armour
    List<int> Tags{
        get;
        //TODO: should this have a set?
    }
    Sprite ItemImage
    {
        get;
        set;
    }
}
