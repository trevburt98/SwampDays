using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedWeapon : IWeapon
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

    void Reload();
}
