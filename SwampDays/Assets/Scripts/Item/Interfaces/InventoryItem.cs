using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string itemId;
    public string itemName;
    public string flavourText;

    public void useItem()
    {
        GameObject obj = (GameObject)Resources.Load(itemId);
        IConsumable consumable = obj.GetComponent<IConsumable>();
        consumable.use();
    }
}
