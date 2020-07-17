using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedWeapon : IWeapon
{
    string[] BulletIDs
    {
        get;
        set;
    }

    int CurrentAmmoType
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

    void Reload(int numToReload);
    void AimDownSight();
}
