using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeWeapon<T> : IWeapon<T>
{
    float StrengthMultiplier
    {
        set;
        get;
    }

    void Block(T damageReduction);

    void HeavyAttack(T damageMultiplier);
}
