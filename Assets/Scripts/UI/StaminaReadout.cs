using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaReadout : MonoBehaviour
{
    private Text text;

    public void initStaminaUI(float startingStamina)
    {
        text = GetComponent<Text>();
        string healthText = startingStamina + " stamina";
        text.text = healthText;
    }

    public void changeStaminaUI(float newStamina)
    {
        string healthText = newStamina + " stamina";
        text.text = healthText;
    }
}
