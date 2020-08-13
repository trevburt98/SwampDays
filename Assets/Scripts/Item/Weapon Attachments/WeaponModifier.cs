using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponModifier
{
    //0 - Damage Modifier
    //1 - Holster Speed Modifier
    //2 - Accuracy Modifier
    //3 - ADS Accuracy Modifier Modifier (fuck you walker)
    //4 - Gun Kick Modifier
    //5 - Reload Speed Modifier
    //6 - Magazine Size Modifier
    //7 - Zoom Modifier
    //8 - Cooldown Between Shots Modifier
    public int modificationType;
    public float modificationAmount;

    public WeaponModifier(int type, float amount)
    {
        modificationType = type;
        modificationAmount = amount;
    }
}
