using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeWeapon : IWeapon
{
    float StrengthMultiplier
    {
        get;
        set;
    }

    float BlockDamageReduction
    {
        get;
        set;
    }

    void Block();

    void HeavyAttack();
}
