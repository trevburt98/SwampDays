using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable
{
    private string _name = "Example Crafting Table";
    public string Name
    {
        get => _name;
    }

    int numIngredients = 3;

    public void Interact(GameObject player)
    {
        PlayerCharacter character = player.GetComponent<PlayerCharacter>();
        character.ToggleCraftingMenu(true, 2);
    }
}
