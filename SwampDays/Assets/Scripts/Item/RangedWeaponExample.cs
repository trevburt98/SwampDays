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

    private int _weaponId = 1;
    public int ID
    {
        get => _weaponId;
        set => _weaponId = value;
    }

    private string _weaponName = "Example Ranged Weapon";
    public string Name
    {
        get => _weaponName;
    }

    private string _flavourText = "Example implementation of a ranged weapon. Used to create a workflow for future weapons";
    public string FlavourText
    {
        get => _flavourText;
    }

    private float _damage = 10;
    public float BaseDamage
    {
        get => _damage;
        set => _damage = value;
    }

    private float _range = 10;
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

    private AudioClip _fireSound;
    public AudioClip Sound
    {
        get => _fireSound;
        set => _fireSound = value;
    }

    private int[] _compatibleBulletArray;
    public int[] BulletIDs
    {
        get => _compatibleBulletArray;
        set => _compatibleBulletArray = value;
    }

    void IWeapon<float>.Attack(float damageDone)
    {
        Transform startObject = transform.GetChild(0);
        Vector3 fwd = startObject.TransformDirection(Vector3.forward);
        Vector3 start = startObject.position;
        Debug.DrawRay(start, fwd, Color.red);
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
