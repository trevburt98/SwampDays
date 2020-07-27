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
    private IBag playerBag;

    private void Start()
    {
        player = playerObject.GetComponent<PlayerCharacter>();
        equipmentManager = playerObject.GetComponent<EquipmentManager>();
        toggleAllButtons(false);
    }

    public void PopulateInventory()
    {
        //Delete the existing inventory
        foreach(Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        List<GameObject> inventoryList = new List<GameObject>();
        foreach(Transform child in player.Bag.transform)
        {
            inventoryList.Add(child.gameObject);
        }

        if(player.Bag != null)
        {
            playerBag = player.Bag.GetComponent<IBag>();
            bagName.text = playerBag.Name;
            numSpaces.text = playerBag.CurrentSpaces.ToString() + "/" + playerBag.MaxSpaces.ToString() + " inventory spaces";
        } else
        {
            bagName.text = "";
            numSpaces.text = "";
        }

        GameObject newObj;

        try
        {
            foreach (GameObject obj in inventoryList)
            {
                IItem item = obj.GetComponent<IItem>();
                //Create a new inventroy list item
                newObj = (GameObject)Instantiate(inventoryItemPrefab, transform);
                //Assign the text on the list item to the name of the item
                newObj.GetComponentInChildren<Text>().text = item.Name;
                //If a stack of items, add the number in the stack to the name
                if (item.NumInStack > 1)
                {
                    newObj.GetComponentInChildren<Text>().text += " x" + item.NumInStack;
                }
                newObj.GetComponentInChildren<Button>().onClick.AddListener(delegate { LoadItemInfo(obj, item); });
            }
        } catch(NullReferenceException e)
        {
            Debug.Log("no bag equipped and we haven't done personal inventory yet");
        }
    }

    void LoadItemInfo(GameObject itemObj, IItem itemInfo)
    {
        //TODO: This can maybe use the item tag system?
        ClearItemInfo();
        if (itemInfo is IConsumable)
        {
            useButton.onClick.AddListener(delegate { UseItem(itemObj); });
            toggleUseButton(true);
        } else if(itemInfo is IEquipment)
        {
            equipButton.onClick.AddListener(delegate { EquipItem(itemObj); });
            toggleEquipButton(true);
        } else if(itemInfo is IWeapon)
        {
            equipButton.onClick.AddListener(delegate { EquipWeapon(itemObj, true); });
            toggleEquipButton(true);
        } else if(itemInfo is IBag)
        {
            //GameObject obj = (GameObject)Resources.Load(item.ID);
            //IBag bag = obj.GetComponent<IBag>();
            equipButton.onClick.AddListener(delegate { EquipBag(itemObj); });
            toggleEquipButton(true);
        }

        itemDescription.text = itemInfo.FlavourText +  "\n" + getTagList(itemInfo.Tags);
        itemImage.sprite = itemInfo.ItemImage;

        dropButton.onClick.AddListener(delegate { DropItem(itemObj); });
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

    void UseItem(GameObject obj)
    {
        IConsumable item = obj.GetComponent<IConsumable>();
        //Call the item's use function
        item.Use(player);

        if (player.Bag.GetComponent<IBag>().Remove(obj, playerObject) <= 0)
        {
            ClearItemInfo();
        }

        //Reload the inventory
        PopulateInventory();
    }

    void EquipItem(GameObject equipment)
    {
        equipmentManager.equipNewEquipment(equipment, equipment.GetComponent<IEquipment>().EquipSlot);
    }

    void EquipWeapon(GameObject weapon, bool mainHand)
    {
        equipmentManager.equipNewWeapon(weapon, mainHand);
    }

    void EquipBag(GameObject newBag)
    {
        //TODO: figure out what to do when the new bag has a smaller capacity than the new bag
        //Change the new bag's parent to the player
        newBag.transform.parent = player.transform;
        //Change the old bag's parent to the new bag
        player.Bag.transform.parent = newBag.transform;

        //Get the inventory in the old bag
        //Not a huge fan of this, but I can't find a better way to do it
        List<GameObject> currentBagInventory = new List<GameObject>();
        foreach (Transform child in player.Bag.transform)
        {
            if (child.gameObject != newBag)
            {
                currentBagInventory.Add(child.gameObject);
            }
        }
        //Set each of the old inventory's parents to the new bag
        foreach(GameObject item in currentBagInventory)
        {
            item.transform.parent = newBag.transform;
        }
        //Set the reference to the bag in player to the new bag
        player.Bag = newBag;
        PopulateInventory();
    }

    void DropItem(GameObject item)
    {
        player.Bag.GetComponent<IBag>().Drop(item, playerObject);
        ClearItemInfo();
        //Reload the inventory
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
