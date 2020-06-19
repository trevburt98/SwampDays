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
}
