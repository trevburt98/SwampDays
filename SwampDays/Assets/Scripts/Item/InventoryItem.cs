using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public string itemId;
    public string itemName;
    public string flavourText;
    public float weight;

    public InventoryItem(string itemId, string itemName, string flavourText, float weight)
    {
        this.itemId = itemId;
        this.itemName = itemName;
        this.flavourText = flavourText;
        this.weight = weight;
    }

    public void useItem(ICharacter<float> user)
    {
        GameObject obj = (GameObject)Resources.Load(itemId);
        IConsumable consumable = obj.GetComponent<IConsumable>();
        consumable.use(user);
    }

    public void dropItem(Vector3 pos)
    {
        GameObject.Instantiate(Resources.Load(itemId), pos, Quaternion.Euler(0, 0, 0));
    }
}
