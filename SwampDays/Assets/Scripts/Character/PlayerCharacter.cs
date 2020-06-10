using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;

namespace Character.PlayerCharacter
{
    public class PlayerCharacter : MonoBehaviour, ICharacter<float>
    {
        #region Trait Declarations
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        //Strength affects how much melee damage a character does and the player character's various carrying tiers
        private int _strength;
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
        private int _endurance;
        public int Endurance
        {
            get => _endurance;
            set => _endurance = value;
        }

        //Vitality affects a character's max health
        private int _vitality;
        public int Vitality
        {
            get => _vitality;
            set => _vitality = value;
        }

        //Move Speed dictates how quickly a character runs
        private int _moveSpeed;
        public int MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }

        private float _stamina;
        private float maxStamina;
        public float Stamina
        {
            get => _stamina;
            set => _stamina = value;
        }
        #endregion

        public float maxHealth;
        public float currentHealth;

        private GameObject equipped = null;
        private List<GameObject> inventory = new List<GameObject>();

        //Reference to the first person controller attached to the character
        [SerializeField] private FirstPersonController fpsController;
        //Reference to UI canvas
        [SerializeField] private Canvas CanvasUI;
        //Reference to the camera of the character
        [SerializeField] private Camera camera;
        //Reference to the hand gameobject
        [SerializeField] private GameObject hand;

        //Reference to the health readout on the UI
        private HealthReadout healthUI;
        //Reference to the stamina readout on the UI
        private StaminaReadout staminaUI;
        //Reference to interaction prompt on UI
        private InteractionPrompt interactionPrompt;

        //TEMP
        //Public versions to quickly change from unity editor
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

            //Get the UI elements for the character
            healthUI = CanvasUI.GetComponentInChildren<HealthReadout>();
            staminaUI = CanvasUI.GetComponentInChildren<StaminaReadout>();
            interactionPrompt = CanvasUI.GetComponentInChildren<InteractionPrompt>();

            //Initialize health and stamina UI elements
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
            //if(Input.GetKeyDown(KeyCode.Mouse0))
            //{
            //    Attack(5.0f);
            //}

            //If we have an item equipped call our equipped update function
            if(equipped != null)
            {
                equippedUpdate();
            }

            //Send out the raycast seeking interactables
            interactableRayCast();
        }

        //Setters for each trait
        #region Trait Setters
        public void setStrength(int newStrength)
        {
            Strength = newStrength;
            //Recalculate the various carrying tiers whenever changing strength
            calculateCarryingTier();
        }

        public void setEndurance(int newEndurance)
        {
            Endurance = newEndurance;
            //TODO: update with acutal endurance -> max stamina algorithm
            maxStamina = Stamina = newEndurance;
        }

        public void setVitality(int newVitality)
        {
            Vitality = newVitality;
            //TODO: update with actual vitality -> max health algorithm
            maxHealth = Vitality * 2;
        }

        public void setMoveSpeed(int newMoveSpeed)
        {
            MoveSpeed = newMoveSpeed;
            //Change the movements speed within the first person controller to reflect changes in the character
            fpsController.changeMoveSpeed(newMoveSpeed);
        }
        #endregion


        #region Public Function
        //ICharacter method for taking damage
        //TODO: implement death
        public void Damage(float damageTaken)
        {
            currentHealth -= damageTaken;
            healthUI.changeHealthUI(currentHealth);
        }

        //TEMP: temporary melee attack function to test functionality of strength
        //TODO: move the attack into ICharacter interface
        public void Attack(float damageBase)
        {
            float damageDone = Random.Range(damageBase - 5, damageBase + 5) + Strength;
            Debug.Log("Did " + damageDone + " damage!");
        }

        //Simple update to indicate that stamina has been changed
        //TODO: prevent this from being called all the time, even when not needed, have to change implementation within fpscontroller
        public void updateStamina(float staminaChange)
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

        //Change the amount that is currently being carried
        public void updateCarryingCapacity(int newCarryingCapacity)
        {
            int prevTier = currentTier;
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
            }
            //Need to decrease our current tier
            else if(currentTier > 0 && currentCarryingCapacity <= tier[currentTier - 1])
            {
                Debug.Log("Going down a tier from " + currentTier);
                for(int i = currentTier; i >= 0; i--)
                {
                    if(currentCarryingCapacity >= tier[i])
                    {
                        currentTier = i;
                        break;
                    }
                }
            }

            //If we've changed tiers, apply the effects of the current tier to the player
            if(prevTier != currentTier)
            {
                applyCurrentTier();
            }
        }

        //Calculate the various carrying tiers
        //TODO: overhaul with an actual algorithm
        public void calculateCarryingTier()
        {
            tier[0] = Strength * 2;
            tier[1] = tier[0] + Strength * 2;
            tier[2] = tier[1] + Strength * 1.5f;
            tier[3] = tier[2] + Strength * 1;
            tier[4] = tier[3] + Strength * 0.5f;
        }

        //Function to apply the constraints of the current tier onto the player
        //TODO: update with actual tier modifiers
        public void applyCurrentTier()
        {
            int i = currentTier;
            fpsController.resetModifiers();
            if(i == 4)
            {
                fpsController.moveSpeedModifier = 0.75f;
                i--;
            }
            if(i == 3)
            {
                fpsController.runSpeedModifier = 0.75f;
                i--;
            }
            if(i == 2)
            {
                fpsController.staminaUseModifier = 1.25f;
                i--;
            }
            if(i == 1)
            {
                fpsController.staminaRecoveryModifier = 0.75f;
            }
        }
        #endregion

        #region Private Functions

        //Function to handle the raycast checking for interactables and the pickup of items
        private void interactableRayCast()
        {
            int layerMask = 1 << 8;
            RaycastHit hit;

            Vector3 forward = camera.transform.TransformDirection(Vector3.forward) * 5;
            Debug.DrawRay(camera.transform.position, forward, Color.green);

            //On Raycast hit
            if (Physics.Raycast(camera.transform.position, forward, out hit, 10, layerMask))
            {
                //Throw up the prompt for this interaction
                interactionPrompt.promptInteraction(hit.transform.name);
                //If the player presses the interaction button
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //If the item is equippable
                    if (hit.transform.gameObject.GetComponent<IInteractable>().Equippable)
                    {
                        //Add the item to the players inventory
                        //TODO: check max inventory space against current to determine whether or not this item can be stowed
                        inventory.Add(hit.transform.gameObject);
                        //If nothing is equipped
                        if (equipped == null)
                        {
                            //Set equipped to the object, make the object a child of the hand
                            equipped = hit.transform.gameObject;
                            hit.transform.parent = hand.transform;
                            hit.transform.localPosition = new Vector3(0, 0, 0);
                            hit.transform.localRotation = Quaternion.Euler(0, 90, 0);
                            hit.rigidbody.useGravity = false;
                            hit.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                        }
                    }
                }
            }
            //Otherwise, remove the interaction prompt from the screen
            else
            {
                interactionPrompt.removePrompt();
            }
        }

        //Update called whenever the player has an item currently equipped
        private void equippedUpdate()
        {
            //The player presses the interaction key while they have something equipped
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Unparent the hand, set equipped to nothing
                //TODO: Remove the specific item from the player's inventory
                Rigidbody rb = equipped.GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.constraints = RigidbodyConstraints.None;
                equipped.transform.parent = null;
                equipped = null;
                int i = 0;
                foreach(GameObject obj in inventory)
                {
                    Debug.Log(++i + ": " + obj);
                }
            }

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                equipped.GetComponent<IRangedWeapon<float>>().Attack(20);
            }
        }
        #endregion
    }
}
