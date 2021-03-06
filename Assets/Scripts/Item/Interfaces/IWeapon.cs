﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon : IItem
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

    //Item Stats
    float HolsterSpeed{
        get;
    }

    float MinHolsterSpeed{
        get;
    }

    float HolsterSpeedModifier{
        get;
        set;
    }

    void Attack(ICharacter<float> character);

    void Break();

    void Repair();
}
