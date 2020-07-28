using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.PlayerCharacter;

public class AlchemyTable : MonoBehaviour
{
    void Start(){}
    void Update(){}

    public void Interact(PlayerCharacter character){
        character.ToggleAlchemyMenu(true);
    }
}
