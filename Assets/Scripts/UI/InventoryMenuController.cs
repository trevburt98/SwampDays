using Character.PlayerCharacter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuController : MonoBehaviour
{
    public GameObject inventoryItemPrefab;
    public GameObject playerObject;

    public Text bagName;
    public Text numSpaces;

    public Image itemImage;
    public Text itemDescription;
    public Button useButton;
    public Button equipButton;
    public Button dropButton;

    private PlayerCharacter player;
    private EquipmentManager equipmentManager;

    private void Start()
    {
        player = playerObject.GetComponent<PlayerCharacter>();
        equipmentManager = playerObject.GetComponent<EquipmentManager>();
        toggleAllButtons(false);
    }

    public void PopulateInventory()
    {
        foreach(Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        if(player.bag != null)
        {
            bagName.text = player.bag.Name;
            numSpaces.text = player.bag.CurrentSpaces.ToString() + "/" + player.bag.MaxSpaces.ToString() + " inventory spaces";
        }
        else
        {
            bagName.text = "";
            numSpaces.text = "";
        }



        GameObject newObj;

        try
        {
            foreach (IInteractable item in player.bag.Inventory)
            {
                newObj = (GameObject)Instantiate(inventoryItemPrefab, transform);
                newObj.GetComponentInChildren<Text>().text = item.Name;
                if (item.NumInStack > 1)
                {
                    newObj.GetComponentInChildren<Text>().text += " x" + item.NumInStack;
                }
                newObj.GetComponentInChildren<Button>().onClick.AddListener(delegate { LoadItemInfo(item); });
            }
        } catch(NullReferenceException e)
        {
            Debug.Log("no bag equipped and we haven't done personal inventory yet");
        }
    }

    void LoadItemInfo(IInteractable item)
    {
        //TODO: This can maybe use the item tag system?
        ClearItemInfo();
        if (item is IConsumable)
        {
            GameObject obj = (GameObject)Resources.Load(item.ID);
            IConsumable consumable = obj.GetComponent<IConsumable>();
            useButton.onClick.AddListener(delegate { UseItem(item, consumable); });
            toggleUseButton(true);
        } else if(item is IEquipment)
        {
            GameObject obj = (GameObject)Resources.Load(item.ID);
            IEquipment equipment = obj.GetComponent<IEquipment>();
            equipButton.onClick.AddListener(delegate { EquipItem(equipment); });
            toggleEquipButton(true);
        } else if(item is IWeapon)
        {
            GameObject obj = (GameObject)Resources.Load(item.ID);
            IWeapon equipment = obj.GetComponent<IWeapon>();
            equipButton.onClick.AddListener(delegate { EquipWeapon(equipment, true); });
            toggleEquipButton(true);
        } else if(item is IBag)
        {
            GameObject obj = (GameObject)Resources.Load(item.ID);
            IBag bag = obj.GetComponent<IBag>();
            equipButton.onClick.AddListener(delegate { EquipBag(bag); });
            toggleEquipButton(true);
        }

        itemDescription.text = item.FlavourText +  "\n" + getTagList(item.Tags);
        itemImage.sprite = item.ItemImage;

        dropButton.onClick.AddListener(delegate { DropItem(item); });
        toggleDropButton(true);
    }

    void ClearItemInfo()
    {
        itemImage.sprite = null;
        itemDescription.text = "";
        useButton.onClick.RemoveAllListeners();
        dropButton.onClick.RemoveAllListeners();
        equipButton.onClick.RemoveAllListeners();

        toggleAllButtons(false);
    }

    void UseItem(IInteractable interactable, IConsumable item)
    {
        item.Use(player);
        if(--interactable.NumInStack <= 0)
        {
            player.removeFromInventory(interactable);
            ClearItemInfo();
        }
        PopulateInventory();
    }

    void EquipItem(IEquipment equipment)
    {
        equipmentManager.equipNewEquipment(equipment, equipment.EquipSlot);
    }

    void EquipWeapon(IWeapon weapon, bool mainHand)
    {
        equipmentManager.equipNewWeapon(weapon, mainHand);
    }

    void EquipBag(IBag bag)
    {
        //TODO: figure out what to do when the new bag has a smaller capacity than the new bag
        //Copy the currentInventory into a temp array
        IInteractable[] currentInventory = player.bag.Inventory.ToArray();
        //Delete everything in the current inventory
        player.bag.Inventory.Clear();
        //Add everything from the temp array into our current bag
        bag.Inventory = new List<IInteractable>(currentInventory);
        //Delete the duplicate bag from the new inventory
        IInteractable oldBag = bag.Inventory.Find(x => x.ID == bag.ID);
        bag.Inventory.Remove(oldBag);
        //Add the previous bag into the player inventory
        bag.Inventory.Add(player.bag);
        //Set the player's inventory bag to the new bag
        player.bag = bag;
        PopulateInventory();
    }

    void DropItem(IInteractable item)
    {
        Vector3 newPos = player.transform.position;
        GameObject newObj = GameObject.Instantiate(Resources.Load(item.ID), newPos, Quaternion.Euler(0, 0, 0)) as GameObject;
        newObj.GetComponent<IInteractable>().NumInStack = item.NumInStack;
        player.removeFromInventory(item);
        ClearItemInfo();
        PopulateInventory();
    }

    void toggleAllButtons(bool toggleOn)
    {
        toggleUseButton(toggleOn);
        toggleEquipButton(toggleOn);
        toggleDropButton(toggleOn);
    }

    void toggleUseButton(bool toggleOn)
    {
        useButton.GetComponent<Image>().enabled = toggleOn;
        useButton.GetComponent<Button>().enabled = toggleOn;
        useButton.GetComponentInChildren<Text>().enabled = toggleOn;
    }

    void toggleEquipButton(bool toggleOn)
    {
        equipButton.GetComponent<Image>().enabled = toggleOn;
        equipButton.GetComponent<Button>().enabled = toggleOn;
        equipButton.GetComponentInChildren<Text>().enabled = toggleOn;
    }

    void toggleDropButton(bool toggleOn)
    {
        dropButton.GetComponent<Image>().enabled = toggleOn;
        dropButton.GetComponent<Button>().enabled = toggleOn;
        dropButton.GetComponentInChildren<Text>().enabled = toggleOn;
    }

    string getTagList(List<int> tags){
        string tagList = "";
        foreach(var tag in tags){
            string tagString = convertTag(tag);
            //TODO: Feels like there is a better way to do this
            if (tagList != "" && tagString != null){
                tagList += ", ";
            }
            if (tagString != null){
                tagList += tagString;
            }
        }
        return tagList;
    }
    string convertTag(int id){
        //TODO: I started to do this with an enum but Stack Overflow said this and it is easy. verify that its cool
        switch (id)
        {
            case 0: return "Rifle";
            case 1: return "Handgun";
            case 2: return "Ammunition";
            case 3: return "Ingredient";
            case 4: return "Healing Item";
            case 5: return "Harmful Item";
            case 6: return "Food";
            case 7: return "Drink";
            case 8: return "Head Armor";
            case 9: return "Body Armor";
            case 10: return "Bag";
            default: return null;
        }
    }
}
