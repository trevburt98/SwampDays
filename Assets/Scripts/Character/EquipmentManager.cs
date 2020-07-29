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
    public GameObject[] equipmentArray = new GameObject[5];

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

    public void equipNewEquipment(GameObject newEquipment, int equipmentSlot)
    {
        equipmentArray[equipmentSlot] = newEquipment;
        player.armourRating += newEquipment.GetComponent<IEquipment>().ArmourValue;
        Debug.Log(player.armourRating);
    }

    public void unequipEquipment(int spotToUnequip)
    {
        player.armourRating -= equipmentArray[spotToUnequip].GetComponent<IEquipment>().ArmourValue;
        equipmentArray[spotToUnequip] = null;
        Debug.Log(player.armourRating);
    }

    public bool equipNewWeapon(GameObject newWeapon, bool inMainHand)
    {

        bool ret = false;
        if (inMainHand && mainHand == null)
        {
            newWeapon.transform.parent = playerHand.transform;
            newWeapon.transform.localPosition = new Vector3(0, 0, 3);
            newWeapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
            newWeapon.GetComponent<Rigidbody>().useGravity = false;
            newWeapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            newWeapon.SetActive(true);

            mainHand = newWeapon;
            ret = true;
        } else if (offHand == null)
        {
            //TODO: put the stuff in the offhand
            offHand = newWeapon;
            ret = true;
        }

        return ret;
    }
}
