using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;

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
        private int currentTier, currentCarryingCapacity;
        private int[] tier = new int[5];

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
        private int maxStamina;
        public int Stamina
        {
            get => _stamina;
            set => _stamina = value;
        }

        public float maxHealth;
        public float currentHealth;

        [SerializeField] private FirstPersonController fpsController;
        [SerializeField] private HealthReadout healthUI;
        [SerializeField] private StaminaReadout staminaUI;

        public int strength;
        public int endurance;
        public int vitality;
        public int moveSpeed;

        void Start()
        {
            setStrength(strength);
            setEndurance(endurance);
            setVitality(vitality);
            setMoveSpeed(moveSpeed);
            currentTier = 0;
            updateCarryingCapacity(20);
            currentHealth = maxHealth;

            healthUI.initHealthUI(maxHealth);
            staminaUI.initStaminaUI(maxStamina);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                updateCarryingCapacity(-10);
            }
            if(Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                updateCarryingCapacity(10);
            }
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack(5.0f);
            }
        }

        #region Trait Setters
        public void setStrength(int newStrength)
        {
            Strength = newStrength;
            calculateCarryingTier();
        }

        public void setEndurance(int newEndurance)
        {
            Endurance = newEndurance;
            maxStamina = Stamina = newEndurance;
        }

        public void setVitality(int newVitality)
        {
            Vitality = newVitality;
            maxHealth = Vitality * 2;
        }

        public void setMoveSpeed(int newMoveSpeed)
        {
            MoveSpeed = newMoveSpeed;
            fpsController.changeMoveSpeed(newMoveSpeed);
        }
        #endregion

        public void Damage(float damageTaken)
        {
            currentHealth -= damageTaken;
            healthUI.changeHealthUI(currentHealth);
        }

        public void Attack(float damageBase)
        {
            float damageDone = Random.Range(damageBase - 5, damageBase + 5) + Strength;
            Debug.Log("Did " + damageDone + " damage!");
        }

        public void updateStamina(int staminaChange)
        {
            Stamina += staminaChange;
            if(Stamina < 0)
            {
                Stamina = 0;
            }

            if(Stamina > maxStamina)
            {
                Stamina = maxStamina;
            }
            staminaUI.changeStaminaUI(Stamina);
        }

        public void updateCarryingCapacity(int newCarryingCapacity)
        {
            currentCarryingCapacity += newCarryingCapacity;
            
            //Need to increase our current tier
            if(currentCarryingCapacity > tier[currentTier])
            {
                for(int i = currentTier; i < tier.Length; i++)
                {
                    if(currentCarryingCapacity <= tier[i])
                    {
                        currentTier = i;
                        break;
                    }
                }
            } else if(currentCarryingCapacity < tier[currentTier - 1])
            {
                for(int i = currentTier; i >= 0; i--)
                {
                    if(currentCarryingCapacity >= tier[i])
                    {
                        currentTier = i;
                        break;
                    }
                }
            }

            Debug.Log("");
            Debug.Log("Current carrying capacity: " + currentCarryingCapacity);
            Debug.Log("Tier Zero: " + tier[0]);
            Debug.Log("Tier One: " + tier[1]);
            Debug.Log("Tier Two: " + tier[2]);
            Debug.Log("Tier Three: " + tier[3]);
            Debug.Log("Tier Four: " + tier[4]);
            Debug.Log(currentTier);
        }

        public void calculateCarryingTier()
        {
            tier[0] = Strength * 2;
            tier[1] = tier[0] + Strength * 2;
            tier[2] = tier[1] + Strength * 2;
            tier[3] = tier[2] + Strength * 2;
            tier[4] = tier[3] + Strength * 2;
        }
    }
}
