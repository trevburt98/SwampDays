using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.PlayerCharacter;

public class AlchemyTable : MonoBehaviour, IInteractable
{
    private string _name = "Alchemy Table";
    public string Name
    {
        get => _name;
    }

    private string _interactPrompt = "Use alchemy table";
    public string InteractPrompt
    {
        get => _interactPrompt;
        set => _interactPrompt = value;
    }

    int numIngredients = 2;

    void Start()
    {
        baseList.Add(new Base1());
        baseList.Add(new BassGuitar());
        baseList.Add(new SeaBase());
        baseList.Add(new Base1());
        baseList.Add(new SeaBase());
    }
    void Update(){}

    List<AlchemyBase> baseList = new List<AlchemyBase>();

    public void Interact(GameObject character){
        PlayerCharacter player = character.GetComponent<PlayerCharacter>();
        player.ToggleAlchemyMenu(true, baseList, numIngredients);
    }
}
