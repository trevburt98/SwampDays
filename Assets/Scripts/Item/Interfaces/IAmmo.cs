using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmmo : IItem
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
