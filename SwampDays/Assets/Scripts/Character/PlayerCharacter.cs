using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.PlayerCharacter
{
    public class PlayerCharacter : MonoBehaviour, ICharacter<float>
{
    private string _name;
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    private int _strength;
    public int Strength
    {
        get => _strength;
        set => _strength = value;
    }

    private int _endurance;
    public int Endurance
    {
        get => _endurance;
        set => _endurance = value;
    }

    private int _vitality;
    public int Vitality
    {
        get => _vitality;
        set => _vitality = value;
    }

    private int _moveSpeed;
    public int MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }


    private int _stamina;

    public float maxHealth;
    public float currentHealth;

    [SerializeField]private HealthReadout healthUI;

    void Start()
    {
        setStrength(5);
        setEndurance(5);
        setVitality(50);
        currentHealth = maxHealth;
        _stamina = 5;

        healthUI.initUI(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Damage(5.0f);
        }
    }

    public void setStrength(int newStrength)
    {
        Strength = newStrength;
    }

    public void setEndurance(int newEndurance)
    {
        Endurance = newEndurance;
    }

    public void setVitality(int newVitality)
    {
        Vitality = newVitality;
        maxHealth = Vitality * 2;
    }

    public void Damage(float damageTaken)
    {
        currentHealth -= damageTaken;
        healthUI.changeHealthUI(currentHealth);
    }
}
}
