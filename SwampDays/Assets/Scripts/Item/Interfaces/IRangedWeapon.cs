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
    
    bool ADS
    {
        get;
        set;
    }

    float Zoom
    {
        get;
        set;
    }

    void Reload();
    void AimDownSight();
}
