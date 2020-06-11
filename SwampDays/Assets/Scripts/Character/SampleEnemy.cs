using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : MonoBehaviour, INpc
{

    #region Trait Declarations
    private string _name;
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    //Strength affects how much melee damage a character does and the player character's various carrying tiers
    private int _strength = 5;
    public int Strength
    {
        get => _strength;
        set => _strength = value;
    }
    //Our current tier of carrying capacity
    private int currentTier;
    //Our current carrying capacity
    public int currentCarryingCapacity;
    //Array representing the maximum carrying capacity of each tier
    private float[] tier = new float[5];

    //Endurance affects a character's max stamina
    private int _endurance = 5;
    public int Endurance
    {
        get => _endurance;
        set => _endurance = value;
    }

    //Vitality affects a character's max health
    private int _vitality = 10;
    public int Vitality
    {
        get => _vitality;
        set => _vitality = value;
    }

    //Move Speed dictates how quickly a character runs
    private int _moveSpeed = 3;
    public int MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    private int _opinion = -100;
    public int Opinion
    {
        get => _opinion;
        set => _opinion = value;
    }
    #endregion

    public float maxHealth;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = currentHealth = Vitality * 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(float damageTaken)
    {
        currentHealth -= damageTaken;
        Debug.Log("you got me, pardner, now i'm " + currentHealth + " health");
        if(currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("well shit, guess i'll die then");
        Destroy(this.gameObject);
    }
}
