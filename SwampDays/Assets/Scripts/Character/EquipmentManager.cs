using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    public IEquipment head;
    public IEquipment torso;
    public IEquipment arms;
    public IEquipment legs;
    public IEquipment feet;
    public IWeapon<float> mainHand;
    public IWeapon<float> offHand;

    void Start()
    {
        head = null;
        torso = null;
        arms = null;
        legs = null;
        feet = null;
        mainHand = null;
        offHand = null;
    }

    public void equipNew(IEquipment newEquipment, int equipmentSlot)
    {
        switch(equipmentSlot)
        {
            case 1:
                head = newEquipment;
                break;
            case 2:
                torso = newEquipment;
                break;
            case 3:
                arms = newEquipment;
                break;
            case 4:
                legs = newEquipment;
                break;
            case 5:
                feet = newEquipment;
                break;
        }

        Debug.Log("Just equipped " + newEquipment);
    }
}
