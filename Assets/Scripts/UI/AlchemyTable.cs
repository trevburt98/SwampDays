using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.PlayerCharacter;

public class AlchemyTable : MonoBehaviour
{
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

    public void Interact(PlayerCharacter character){
        character.ToggleAlchemyMenu(true, baseList);
    }
}
