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

    int AmmoCount
    {
        get;
        set;
    }

    bool Holster{
        get;
        set;
    }

    //Item Stats
    float DamageModifier
    {
        get;
        set;
    }

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

    bool HasBarrelAttachment
    {
        get;
    }

    float CooldownBetweenShots
    {
        get;
    }

    float CooldownBetweenShotsModifier
    {
        get;
        set;
    }

    WeaponAttachment CurrentBarrelAttachment
    {
        get;
        set;
    }

    bool HasGripAttachment
    {
        get;
    }

    WeaponAttachment CurrentGripAttachment
    {
        get;
        set;
    }

    bool HasMagazineAttachment
    {
        get;
    }

    WeaponAttachment CurrentMagazineAttachment
    {
        get;
        set;
    }

    bool HasSightAttachment
    {
        get;
    }

    WeaponAttachment CurrentSightAttachment
    {
        get;
        set;
    }

    bool HasStockAttachment
    {
        get;
    }

    WeaponAttachment CurrentStockAttachment
    {
        get;
        set;
    }

    void Reload(int numToReload, ICharacter<float> character);
    void toggleADS(ICharacter<float> character);
    void HolsterWeapon(ICharacter<float> character);
    void ModifyWeapon(GameObject newAttachment);
    bool Modifiable();
}
