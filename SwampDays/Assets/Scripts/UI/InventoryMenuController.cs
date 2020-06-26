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

        GameObject newObj;

        foreach (IInteractable item in player.inventory)
        {
            newObj = (GameObject)Instantiate(inventoryItemPrefab, transform);
            newObj.GetComponentInChildren<Text>().text = item.Name;
            newObj.GetComponentInChildren<Button>().onClick.AddListener(delegate { LoadItemInfo(item); });
        }
    }

    void LoadItemInfo(IInteractable item)
    {
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
        } else if(item is IWeapon<float>)
        {
            GameObject obj = (GameObject)Resources.Load(item.ID);
            IWeapon<float> equipment = obj.GetComponent<IWeapon<float>>();
            equipButton.onClick.AddListener(delegate { EquipWeapon(equipment, true); });
            toggleEquipButton(true);
        }

        itemDescription.text = item.FlavourText;
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
        item.use(player);
        player.removeFromInventory(interactable);
        ClearItemInfo();
        PopulateInventory();
    }

    void EquipItem(IEquipment equipment)
    {
        equipmentManager.equipNewEquipment(equipment, equipment.EquipSlot);
    }

    void EquipWeapon(IWeapon<float> weapon, bool mainHand)
    {
        equipmentManager.equipNewWeapon(weapon, mainHand);
    }

    void DropItem(IInteractable item)
    {
        Vector3 newPos = player.transform.position;
        GameObject.Instantiate(Resources.Load(item.ID), newPos, Quaternion.Euler(0, 0, 0));
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
}
