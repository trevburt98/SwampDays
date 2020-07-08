using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private GameObject playerHand;
    [SerializeField] private PlayerCharacter player;

    //0 - Head
    //1 - Torso
    //2 - Arms
    //3 - Legs
    //4 - Feet
    public IEquipment[] equipmentArray = new IEquipment[5];

    public GameObject mainHand;
    public GameObject offHand;

    void Start()
    {
        for(int i = 0; i < equipmentArray.Length; i++)
        {
            equipmentArray[i] = null;
        }

        mainHand = null;
        offHand = null;
    }

    public void equipNewEquipment(IEquipment newEquipment, int equipmentSlot)
    {
        equipmentArray[equipmentSlot] = newEquipment;

        player.armourRating += newEquipment.ArmourValue;
        Debug.Log(player.armourRating);
    }

    public void unequipEquipment(int spotToUnequip)
    {
        player.armourRating -= equipmentArray[spotToUnequip].ArmourValue;
        equipmentArray[spotToUnequip] = null;
        Debug.Log(player.armourRating);
    }

    public void equipNewWeapon(IWeapon newWeapon, bool inMainHand)
    {
        GameObject obj = (GameObject)Resources.Load(newWeapon.ID);
        GameObject objInGame = GameObject.Instantiate(obj);
        objInGame.transform.parent = playerHand.transform;
        objInGame.transform.localPosition = new Vector3(0, 0, 0);
        objInGame.transform.localRotation = Quaternion.Euler(0, 90, 0);
        objInGame.GetComponent<Rigidbody>().useGravity = false;
        objInGame.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        if (inMainHand)
        {
            mainHand = objInGame;
        }
        else
        {
            offHand = objInGame;
        }
    }
}
