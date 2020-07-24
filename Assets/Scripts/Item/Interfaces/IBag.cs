using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBag : IItem
{
    int MaxSpaces
    {
        get;
        set;
    }

    int CurrentSpaces
    {
        get;
        set;
    }

    float TotalWeight
    {
        get;
        set;
    }

    void Remove(GameObject item);

    GameObject Find(string itemID);

    bool Add(GameObject itemToAdd);
}
