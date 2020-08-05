using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string Name
    {
        get;
    }

    string InteractPrompt
    {
        get;
        set;
    }

    void Interact(GameObject user);
}
