using System.Collections;
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

    float PistolSkill
    {
        get;
        set;
    }
        
    float RifleSkill
    {
        get;
        set;
    }

    float HeavyRifleSkill
    {
        get;
        set;
    }

    float ShotgunSkill
    {
        get;
        set;
    }

    void Damage(T damageTaken);

    void Heal(T healthHealed);

    void Die();
}
