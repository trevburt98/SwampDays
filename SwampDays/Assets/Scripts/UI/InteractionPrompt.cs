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

    public void promptInteraction(string interactableString)
    {
        string outString = "[" + interactableString + "]";
        text.text = outString;
    }

    public void removePrompt()
    {
        text.text = "";
    }
}
