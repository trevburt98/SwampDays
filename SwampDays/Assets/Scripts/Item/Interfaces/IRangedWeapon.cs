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

    float Accuracy
    {
        get;
        set;
    }

    int MagazineSize
    {
        get;
        set;
    }

    int AmmoCount
    {
        get;
        set;
    }

    //Inherited from IWeapon

    float Range
    {
        get;
    }

    void Reload();
}
