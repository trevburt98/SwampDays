using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuController : MonoBehaviour
{
    public GameObject prefab;
    public GameObject playerObject;

    public Image itemImage;
    public Text itemDescription;
    public Button useButton;
    public Button dropButton;

    private PlayerCharacter player;

    private void Start()
    {
        player = playerObject.GetComponent<PlayerCharacter>();    
    }

    public void PopulateInventory()
    {
        foreach(Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject newObj;

        foreach (InventoryItem item in player.inventory)
        {
            newObj = (GameObject)Instantiate(prefab, transform);
            newObj.GetComponentInChildren<Text>().text = item.itemName;
            newObj.GetComponentInChildren<Button>().onClick.AddListener(delegate { LoadItemInfo(item); });
        }
    }

    void LoadItemInfo(InventoryItem item)
    {
        ClearItemInfo();
        itemDescription.text = item.flavourText;
        useButton.onClick.AddListener(delegate { UseItem(item); });
        dropButton.onClick.AddListener(delegate { DropItem(item); });
    }

    void ClearItemInfo()
    {
        itemImage = null;
        itemDescription.text = "";
        useButton.onClick.RemoveAllListeners();
        dropButton.onClick.RemoveAllListeners();
    }

    void UseItem(InventoryItem item)
    {
        item.useItem(player);
        player.removeFromInventory(item);
        ClearItemInfo();
        PopulateInventory();
    }

    void DropItem(InventoryItem item)
    {
        Vector3 newPos = player.transform.position;
        item.dropItem(newPos);
        player.removeFromInventory(item);
        ClearItemInfo();
        PopulateInventory();
    }
}
