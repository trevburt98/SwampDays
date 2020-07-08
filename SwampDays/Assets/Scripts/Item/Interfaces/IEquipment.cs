using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IEquipment : IInteractable
{
    //0 - Head
    //1 - Torso
    //2 - Arms
    //3 - Legs
    //4 - Feet
    //5 - Main Hand
    //6 - Off Hand
    int EquipSlot
    {
        get;
    }

    int ArmourValue
    {
        get;
        set;
    }

    int Durability
    {
        get;
        set;
    }
}
