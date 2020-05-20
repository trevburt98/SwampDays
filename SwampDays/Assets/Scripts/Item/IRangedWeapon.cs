using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedWeapon<T> : IWeapon<T>
{
    int[] BulletID
    {
        get;
        set;
    }

    void Reload();
}
