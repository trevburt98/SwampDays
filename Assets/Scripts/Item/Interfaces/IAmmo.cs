using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmmo : IInteractable
{
    float DamageMultiplier
    {
        get;
    }

    int Effect
    {
        get;
    }
}
