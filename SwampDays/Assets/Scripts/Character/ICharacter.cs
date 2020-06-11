﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter<T>
{
    string Name
    {
        get;
        set;
    }

    int Strength
    {
        get;
        set;
    }

    int Endurance
    {
        get;
        set;
    }

    int Vitality
    {
        get;
        set;
    }

    int MoveSpeed
    {
        get;
        set;
    }

    void Damage(T damageTaken);

    void Die();
}
