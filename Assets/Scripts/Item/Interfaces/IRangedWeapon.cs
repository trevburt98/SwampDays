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

    //Item Stats
    float Accuracy
    {
        get;
    }

    float MinAccuracy{
        get;
    }

    float AccuracyModifier{
        get;
        set;
    }

    float ADSSpeed{
        get;
    }

    float MinADSSpeed{
        get;
    }

    float GunKick{
        get;
    }

    float MinGunKick{
        get;
    }

    float GunKickModifier{
        get;
        set;
    }

    float ReloadSpeed{
        get;
    }

    float MinReloadSpeed{
        get;
    }

    float ReloadSpeedModifier{
        get;
        set;
    }

    int MagazineSize
    {
        get;
    }

    int MagazineSizeModifier{
        get;
        set;
    }

    float Zoom
    {
        get;
        set;
    }

    float ZoomModifier{
        get;
        set;
    }

    void Reload(ICharacter<float> character);
    void AimDownSight(ICharacter<float> character);
}
