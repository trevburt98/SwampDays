using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthReadout : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void initUI(float currentHealth)
    {
        string healthText = currentHealth + " health";
        text.text = healthText;
    }

    public void changeHealthUI(float newHealth)
    {
        string healthText = newHealth + " health";
        text.text = healthText;
    }
}
