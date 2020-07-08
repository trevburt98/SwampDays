using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthReadout : MonoBehaviour
{
    private Text text;

    public void initHealthUI(float startingHealth)
    {
        text = GetComponent<Text>();
        string healthText = startingHealth + " health";
        text.text = healthText;
    }

    public void changeHealthUI(float newHealth)
    {
        string healthText = newHealth + " health";
        text.text = healthText;
    }
}
