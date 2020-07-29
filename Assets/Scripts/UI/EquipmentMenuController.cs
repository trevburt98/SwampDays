using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentMenuController : MonoBehaviour
{
    public EquipmentManager equipmentManager;
    public PlayerCharacter player;

    public GameObject inventoryItemPrefab;

    public Text equipmentName;
    public Text equipmentDescription;

    public Button headButton;
    public Button torsoButton;
    public Button leftArmButton;
    public Button rightArmButton;
    public Button pantsButton;
    public Button shoesButton;

    public Button equipButton;
    public Button unequipButton;

    private int prevFiltered = -1;

    public void PopulateEquipment()
    {
        //TODO: Change this so that we aren't destroying and recreating the inventory every time the menu is opened
        //Clear the former list to prevent any duplicates
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //TODO: Change this so that it doesn't use a switch statement, maybe an array of references to the different images?
        //Populate the buttons with the images of the equipments that are currently equipped
        foreach (GameObject equipmentObj in equipmentManager.equipmentArray)
        {
            if(equipmentObj != null)
            {
                IEquipment equipment = equipmentObj.GetComponent<IEquipment>();
                switch (equipment.EquipSlot)
                {
                    case 0:
                        headButton.GetComponent<Image>().sprite = equipment.ItemImage;
                        break;
                    case 1:
                        torsoButton.GetComponent<Image>().sprite = equipment.ItemImage;
                        break;
                    case 2:
                        leftArmButton.GetComponent<Image>().sprite = equipment.ItemImage;
                        rightArmButton.GetComponent<Image>().sprite = equipment.ItemImage;
                        break;
                    case 3:
                        pantsButton.GetComponent<Image>().sprite = equipment.ItemImage;
                        break;
                    case 4:
                        shoesButton.GetComponent<Image>().sprite = equipment.ItemImage;
                        break;
                }
            }
        }

        GameObject newObj;

        //Repopulate the list
        foreach (Transform itemObj in player.Bag.transform)
        {
            IItem item = itemObj.gameObject.GetComponent<IItem>();
            //If the item in the inventory is either an equipment or a weapon, display it in the equipment menu list
            if (item is IEquipment || item is IWeapon)
            {
                newObj = (GameObject)Instantiate(inventoryItemPrefab, transform);
                newObj.GetComponentInChildren<Text>().text = item.Name;
                newObj.GetComponentInChildren<Button>().onClick.AddListener(delegate { LoadItemInfo(itemObj.gameObject, item); });
            }
        }

        prevFiltered = -1;
    }

    void LoadItemInfo(GameObject itemObj, IItem item)
    {
        equipmentName.text = item.Name;
        equipmentDescription.text = item.FlavourText;
        clearButtons();

        if (item is IEquipment)
        {
            IEquipment equipment = itemObj.GetComponent<IEquipment>();
            GameObject currentlyEquipped = null;
            //Check if anything is equipped in the selected item's equipment slot
            currentlyEquipped = equipmentManager.equipmentArray[equipment.EquipSlot];

            //If there is nothing currently equipped in that particular slot
            if (currentlyEquipped == null)
            {
                toggleEquipButton(true);
                toggleUnequipButton(false);
                equipButton.onClick.AddListener(delegate { EquipItem(itemObj, equipment); });
            }
            //If something is equipped in that slot
            else
            {
                if (currentlyEquipped == itemObj)
                {
                    toggleEquipButton(false);
                    toggleUnequipButton(true);
                    unequipButton.onClick.AddListener(delegate { UnequipItem(equipment); });
                }
                else
                {
                    toggleEquipButton(true);
                    toggleUnequipButton(false);
                    equipButton.onClick.AddListener(delegate { EquipItem(itemObj, equipment); });
                }
            }
        }
    }

    void EquipItem(GameObject equipmentObj, IEquipment equipment)
    {
        equipmentManager.equipNewEquipment(equipmentObj, equipment.EquipSlot);
        //TODO: Same as the previous switch statement
        switch (equipment.EquipSlot)
        {
            case 0:
                headButton.GetComponent<Image>().sprite = equipment.ItemImage;
                break;
            case 1:
                torsoButton.GetComponent<Image>().sprite = equipment.ItemImage;
                break;
            case 2:
                leftArmButton.GetComponent<Image>().sprite = equipment.ItemImage;
                rightArmButton.GetComponent<Image>().sprite = equipment.ItemImage;
                break;
            case 3:
                pantsButton.GetComponent<Image>().sprite = equipment.ItemImage;
                break;
            case 4:
                shoesButton.GetComponent<Image>().sprite = equipment.ItemImage;
                break;
        }
        toggleUnequipButton(true);
        unequipButton.onClick.AddListener(delegate { UnequipItem(equipment); });
        toggleEquipButton(false);
    }

    void UnequipItem(IEquipment equipment)
    {
        equipmentManager.unequipEquipment(equipment.EquipSlot);
        //TODO: Same as the previous switch statement
        switch (equipment.EquipSlot)
        {
            case 0:
                headButton.GetComponent<Image>().sprite = null;
                break;
            case 1:
                torsoButton.GetComponent<Image>().sprite = null;
                break;
            case 2:
                leftArmButton.GetComponent<Image>().sprite = null;
                rightArmButton.GetComponent<Image>().sprite = null;
                break;
            case 3:
                pantsButton.GetComponent<Image>().sprite = null;
                break;
            case 4:
                shoesButton.GetComponent<Image>().sprite = null;
                break;
        }
        toggleUnequipButton(false);
        toggleEquipButton(true);
    }

    void EquipWeapon(GameObject weapon, bool mainHand)
    {
        equipmentManager.equipNewWeapon(weapon, mainHand);
    }

    void toggleEquipButton(bool toggleOn)
    {
        equipButton.GetComponent<Image>().enabled = toggleOn;
        equipButton.GetComponent<Button>().enabled = toggleOn;
        equipButton.GetComponentInChildren<Text>().enabled = toggleOn;
    }

    void toggleUnequipButton(bool toggleOn)
    {
        unequipButton.GetComponent<Image>().enabled = toggleOn;
        unequipButton.GetComponent<Button>().enabled = toggleOn;
        unequipButton.GetComponentInChildren<Text>().enabled = toggleOn;
    }

    void clearButtons()
    {
        equipButton.onClick.RemoveAllListeners();
        unequipButton.onClick.RemoveAllListeners();
    }

    public void FilterToBodypart(int equipSlot)
    {
        if(prevFiltered == equipSlot)
        {
            PopulateEquipment();
        } else
        {
            foreach (Transform child in this.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            GameObject newObj;

            //Repopulate the list
            foreach (GameObject obj in player.Bag.transform)
            {
                IItem item = obj.GetComponent<IItem>();
                //If the item in the inventory is either an equipment or a weapon, display it in the equipment menu list
                if (item is IEquipment)
                {
                    IEquipment equipmentVersion = item as IEquipment;
                    if (equipmentVersion.EquipSlot == equipSlot)
                    {
                        newObj = (GameObject)Instantiate(inventoryItemPrefab, transform);
                        newObj.GetComponentInChildren<Text>().text = item.Name;
                        newObj.GetComponentInChildren<Button>().onClick.AddListener(delegate { LoadItemInfo(obj, item); });
                    }
                }
            }
            prevFiltered = equipSlot;
        }
    }
}
