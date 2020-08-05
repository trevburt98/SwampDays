using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPrompt : MonoBehaviour
{
    private Text text;

    public bool currentlyDisplaying = false;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void displayPrompt(string Prompt)
    {
        string outString = "[E] " + Prompt;
        text.text = outString;
        currentlyDisplaying = true;
    }

    public void removePrompt()
    {
        text.text = "";
        currentlyDisplaying = false;
    }
}
