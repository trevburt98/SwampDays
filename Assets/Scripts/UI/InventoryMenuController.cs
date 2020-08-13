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
    public GameObject useButtonObj;
    public GameObject equipButtonObj;
    public GameObject dropButtonObj;
    public GameObject modifyButtonObj;

    private Button useButton;
    private Button equipButton;
    private Button dropButton;
    private Button modifyButton;

    public RangedWeaponModifyController modifyController;

    private PlayerCharacter player;
    private EquipmentManager equipmentManager;
    private IBag playerBag;

    private void Start()
    {
        player = playerObject.GetComponent<PlayerCharacter>();
        equipmentManager = playerObject.GetComponent<EquipmentManager>();

        useButton = useButtonObj.GetComponent<Button>();
        equipButton = equipButtonObj.GetComponent<Button>();
        dropButton = dropButtonObj.GetComponent<Button>();
        modifyButton = modifyButtonObj.GetComponent<Button>();


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
            ToggleButton(useButtonObj, true);
        } else if(itemInfo is IEquipment)
        {
            equipButton.onClick.AddListener(delegate { EquipItem(itemObj); });
            ToggleButton(equipButtonObj, true);
        } else if(itemInfo is IWeapon)
        {
            equipButton.onClick.AddListener(delegate { EquipWeapon(itemObj, true); });
            IRangedWeapon rangedWeapon = itemObj.GetComponent<IRangedWeapon>();
            if(rangedWeapon.Modifiable())
            {
                modifyButtonObj.GetComponent<Button>().onClick.AddListener(delegate { ToggleWeaponModifyCanvas(itemObj); });
                ToggleButton(modifyButtonObj, true);
            }
            ToggleButton(equipButtonObj, true);
        } else if(itemInfo is IBag)
        {
            //GameObject obj = (GameObject)Resources.Load(item.ID);
            //IBag bag = obj.GetComponent<IBag>();
            equipButton.onClick.AddListener(delegate { EquipBag(itemObj); });
            ToggleButton(equipButtonObj, true);
        }

        itemDescription.text = itemInfo.FlavourText +  "\n" + getTagList(itemInfo.Tags);
        itemImage.sprite = itemInfo.ItemImage;

        dropButton.onClick.AddListener(delegate { DropItem(itemObj); });
        ToggleButton(dropButtonObj, true);
    }

    void ClearItemInfo()
    {
        itemImage.sprite = null;
        itemDescription.text = "";
        useButton.onClick.RemoveAllListeners();
        dropButton.onClick.RemoveAllListeners();
        equipButton.onClick.RemoveAllListeners();
        modifyButton.onClick.RemoveAllListeners();

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

    void ToggleWeaponModifyCanvas(GameObject objectToModify)
    {

    }

    void DropItem(GameObject item)
    {
        player.Bag.GetComponent<IBag>().Drop(item, playerObject);
        ClearItemInfo();
        //Reload the inventory
        PopulateInventory();
    }

    void ToggleButton(GameObject buttonObj, bool toggleOn)
    {
        buttonObj.SetActive(toggleOn);
    }

    void toggleAllButtons(bool toggleOn)
    {
        ToggleButton(useButtonObj, toggleOn);
        ToggleButton(equipButtonObj, toggleOn);
        ToggleButton(dropButtonObj, toggleOn);
        ToggleButton(modifyButtonObj, toggleOn);
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
            case 3: return "Alchemy Ingredient";
            case 4: return "Healing Item";
            case 5: return "Harmful Item";
            case 6: return "Food";
            case 7: return "Drink";
            case 8: return "Head Armor";
            case 9: return "Body Armor";
            case 10: return "Bag";
            case 11: return "Alchemy Craftable";
            case 12: return "Crafting Ingredient";
            case 13: return "Attachment";
            case 14: return "Stock Attachment";
            case 15: return "Sight Attachment";
            case 16: return "Magazine Attachment";
            case 17: return "Grip Attachment";
            case 18: return "Barrel Attachment";
            default: return null;
        }
    }
}
