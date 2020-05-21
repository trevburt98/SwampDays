using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedWeapon<T> : IWeapon<T>
{
    float Range
    {
        get;
        set;
    }

    int[] BulletID
    {
        get;
        set;
    }

    void Reload();
}
