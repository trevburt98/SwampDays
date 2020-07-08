using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPrompt : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void promptPickup(string interactableString)
    {
        string outString = "[E] Pickup " + interactableString;
        text.text = outString;
    }

    public void promptTalk(string NpcName)
    {
        string outString = "[E] Talk to " + NpcName;
        text.text = outString;
    }

    public void removePrompt()
    {
        text.text = "";
    }
}
