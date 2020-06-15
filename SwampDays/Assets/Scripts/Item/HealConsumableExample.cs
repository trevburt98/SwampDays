using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealConsumableExample : MonoBehaviour, IConsumable
{
    private bool _equippable = false;
    public bool Equippable
    {
        get => _equippable;
    }

    private string _itemId = "consHealEx";
    public string ID
    {
        get => _itemId;
    }

    private string _itemName = "Example Healing Consumable";
    public string Name
    {
        get => _itemName;
    }

    private string _flavourText = "Example implementation of a consumable that heals the player. Used to create a workflow for future consumables";
    public string FlavourText
    {
        get => _flavourText;
        set => _flavourText = value;
    }

    private PlayerCharacter character;

    // Start is called before the first frame update
    void Start()
    {
        character = FindObjectOfType<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void use()
    {
        character.Heal(10);
        Destroy(this.gameObject);
    }
}
