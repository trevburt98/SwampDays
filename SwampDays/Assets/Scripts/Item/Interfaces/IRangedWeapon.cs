using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedWeapon<T> : IWeapon<T>
{
    int[] BulletIDs
    {
        get;
        set;
    }

    bool ADS
    {
        get;
    }
    void Reload();
    void AimDownSight();
}
