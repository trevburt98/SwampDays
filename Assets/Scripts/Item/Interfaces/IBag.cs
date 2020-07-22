using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBag : IInteractable
{
    List<IInteractable> Inventory
    {
        get;
        set;
    }

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

    void Remove(IInteractable item);

    GameObject Find(string itemID);

    bool Add(GameObject itemToAdd);
}
