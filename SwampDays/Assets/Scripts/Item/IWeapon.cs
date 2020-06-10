using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon<T> : IInteractable
{
    int ID
    {
        get;
        set;
    }

    string Name
    {
        get;
        set;
    }

    string FlavourText
    {
        get;
        set;
    }

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

    float Weight
    {
        get;
        set;
    }

    bool Broken
    {
        get;
        set;
    }

    void Attack(T damageDone);

    void Break();

    void Repair();
}
