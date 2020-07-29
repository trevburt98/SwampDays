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

    int Remove(GameObject item, GameObject user);

    GameObject Find(string itemID);

    bool Add(GameObject itemToAdd, GameObject user);

    void Drop(GameObject item, GameObject user);
}
