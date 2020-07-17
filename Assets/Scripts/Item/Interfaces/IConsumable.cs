using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable : IInteractable
{
    void Use(ICharacter<float> user);
}
