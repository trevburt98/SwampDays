using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon<T> : IInteractable
{
    float BaseDamage
    {
        get;
        set;
    }

    float Range
    {
        get;
        set;
    }

    float Durability
    {
        get;
        set;
    }

    bool Broken
    {
        get;
        set;
    }

    AudioClip Sound
    {
        get;
        set;
    }

    //Inherited from IInteractable

    void Attack(T damageDone);

    void Break();

    void Repair();
}
