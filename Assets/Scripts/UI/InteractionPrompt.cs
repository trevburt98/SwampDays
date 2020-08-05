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

    public void promptTalk(string NpcName)
    {
        string outString = "[E] Talk to " + NpcName;
        text.text = outString;
    }

    public void displayPrompt(string Prompt)
    {
        string outString = "[E] " + Prompt;
        text.text = outString;
    }

    public void removePrompt()
    {
        text.text = "";
    }
}
