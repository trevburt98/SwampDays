using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeWeapon : IWeapon
{
    float StrengthMultiplier
    {
        set;
        get;
    }

    float BlockDamageReduction
    {
        get;
        set;
    }

    bool Blocking
    {
        get;
        set;
    }

    void Block(ICharacter<float> character);

    void HeavyAttack(ICharacter<float> character);
}
