using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponExample : MonoBehaviour, IRangedWeapon<float>
{
    private bool _equippable = true;
    public bool Equippable
    {
        get => _equippable;
    }

    private int _weaponId;
    public int ID
    {
        get => _weaponId;
        set => _weaponId = value;
    }

    private string _weaponName;
    public string Name
    {
        get => _weaponName;
        set => _weaponName = value;
    }

    private string _flavourText;
    public string FlavourText
    {
        get => _flavourText;
        set => _flavourText = value;
    }

    private float _damage;
    public float BaseDamage
    {
        get => _damage;
        set => _damage = value;
    }

    private float _range;
    public float Range
    {
        get => _range;
        set => _range = value;
    }

    private float _durability;
    public float Durability
    {
        get => _durability;
        set => _durability = value;
    }

    private float _weight;
    public float Weight
    {
        get => _weight;
        set => _weight = value;
    }

    private bool _broken;
    public bool Broken
    {
        get => _broken;
        set => _broken = value;
    }

    private int[] _compatibleBulletArray;
    public int[] BulletIDs
    {
        get => _compatibleBulletArray;
        set => _compatibleBulletArray = value;
    }

    void IWeapon<float>.Attack(float damageDone)
    {

    }

    void IWeapon<float>.Break()
    {

    }

    void IWeapon<float>.Repair()
    {

    }

    void IRangedWeapon<float>.Reload()
    {

    }
}
