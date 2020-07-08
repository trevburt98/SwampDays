using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeWeapon<T> : IWeapon
{
    float StrengthMultiplier
    {
        set;
        get;
    }

    float blockDamageReduction
    {
        get;
        set;
    }

    float strengthMultiplier
    {
        get;
        set;
    }

    void Block();

    void HeavyAttack();
}
