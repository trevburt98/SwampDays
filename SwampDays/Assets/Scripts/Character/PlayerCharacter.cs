using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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
        public float currentCarryingCapacity;
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

        public int armourRating;

        public EquipmentManager equipment;
        public List<IInteractable> inventory = new List<IInteractable>();

        //Reference to the first person controller attached to the character
        [SerializeField] private FirstPersonController fpsController;
        //Reference to HUD UI canvas
        [SerializeField] private Canvas HUDCanvasUI;
        //Reference to Player Menu UI Canvas
        [SerializeField] private Canvas playerMenuCanvas;
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
        //Reference to the conversation controller controlling the conversation canvas
        [SerializeField] private ConversationController conversationController;

        //TEMP
        //Public versions to quickly change from unity editor
        public int strength;
        public int endurance;
        public int vitality;
        public int moveSpeed;

        private bool inMenu = false;

        void Start()
        {
            setStrength(strength);
            setEndurance(endurance);
            setVitality(vitality);
            setMoveSpeed(moveSpeed);
            currentTier = 0;
            updateCarryingCapacity(0);
            currentHealth = maxHealth;

            //Get the UI elements for the character
            healthUI = HUDCanvasUI.GetComponentInChildren<HealthReadout>();
            staminaUI = HUDCanvasUI.GetComponentInChildren<StaminaReadout>();
            interactionPrompt = HUDCanvasUI.GetComponentInChildren<InteractionPrompt>();

            //Initialize health and stamina UI elements
            healthUI.initHealthUI(maxHealth);
            staminaUI.initStaminaUI(maxStamina);

            armourRating = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                TogglePlayerMenu(!inMenu);
            }

            if (!inMenu)
            {
                if (Input.GetKeyDown(KeyCode.KeypadMinus))
                {
                    updateCarryingCapacity(-10);
                }
                if (Input.GetKeyDown(KeyCode.KeypadPlus))
                {
                    updateCarryingCapacity(10);
                }
                //if(Input.GetKeyDown(KeyCode.Mouse0))
                //{
                //    Attack(5.0f);
                //}

                //If we have an item equipped call our equipped update function
                if (equipment.mainHand != null)
                {
                    equippedUpdate();
                }

                //Send out the raycast seeking interactables
                interactableRayCast();
            }
        }

        //Setters for each trait
        #region Trait Setters
        private void setStrength(int newStrength)
        {
            Strength = newStrength;
            //Recalculate the various carrying tiers whenever changing strength
            calculateCarryingTier();
        }

        private void setEndurance(int newEndurance)
        {
            Endurance = newEndurance;
            //TODO: update with acutal endurance -> max stamina algorithm
            maxStamina = Stamina = newEndurance;
        }

        private void setVitality(int newVitality)
        {
            Vitality = newVitality;
            //TODO: update with actual vitality -> max health algorithm
            maxHealth = Vitality * 2;
        }

        private void setMoveSpeed(int newMoveSpeed)
        {
            MoveSpeed = newMoveSpeed;
            //Change the movements speed within the first person controller to reflect changes in the character
            fpsController.changeMoveSpeed(newMoveSpeed);
        }
        #endregion

        #region Public Function
        //ICharacter method for taking damage
        public void Damage(float damageTaken)
        {
            currentHealth -= damageTaken;
            healthUI.changeHealthUI(currentHealth);
        }

        public void Heal(float healthHealed)
        {
            currentHealth += healthHealed;
            if(currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthUI.changeHealthUI(currentHealth);
        }

        //ICharacter method for dying
        //TODO: implement death
        public void Die()
        {
            Debug.Log("oof");
        }

        //TEMP: temporary melee attack function to test functionality of strength
        //TODO: move the attack into ICharacter interface
        public void Attack(float damageBase)
        {
            float damageDone = Random.Range(damageBase - 5, damageBase + 5) + Strength;
            Debug.Log("Did " + damageDone + " damage!");
        }

        public void removeFromInventory(IInteractable item)
        {
            inventory.Remove(item);
            updateCarryingCapacity(-item.Weight);
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
        #endregion

        #region Private Functions
        //Change the amount that is currently being carried
        private void updateCarryingCapacity(float newCarryingCapacity)
        {
            int prevTier = currentTier;
            currentCarryingCapacity += newCarryingCapacity;

            //Zero check
            if(currentCarryingCapacity < 0)
            {
                currentCarryingCapacity = 0;
            }

            //Need to increase our current tier
            if(currentCarryingCapacity > tier[currentTier])
            {
                for(int i = currentTier; i < tier.Length; i++)
                {
                    if(currentCarryingCapacity <= tier[i] || i == 4)
                    {
                        currentTier = i;
                        break;
                    }
                }
            }
            //Need to decrease our current tier
            else if(currentTier > 0 && currentCarryingCapacity <= tier[currentTier - 1])
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

            //If we've changed tiers, apply the effects of the current tier to the player
            if(prevTier != currentTier)
            {
                applyCurrentTier();
            }
        }

        //Calculate the various carrying tiers
        //TODO: overhaul with an actual algorithm
        private void calculateCarryingTier()
        {
            tier[0] = Strength * 2;
            tier[1] = tier[0] + Strength * 2;
            tier[2] = tier[1] + Strength * 1.5f;
            tier[3] = tier[2] + Strength * 1;
            tier[4] = tier[3] + Strength * 0.5f;
        }

        //Function to apply the constraints of the current tier onto the player
        //TODO: update with actual tier modifiers
        private void applyCurrentTier()
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
                //If the raycast hit an interactable object
                if(hit.transform.GetComponent<IInteractable>() != null)
                {
                    IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();

                    //Throw up the prompt for this interaction
                    interactionPrompt.promptPickup(interactable.Name);
                    //If the player presses the interaction button
                    if (Input.GetKeyDown(KeyCode.E))
                    { 
                        inventory.Add(interactable);
                        updateCarryingCapacity(interactable.Weight);
                        Destroy(hit.transform.gameObject);
                    }
                }
                //If the raycast instead hit an NPC that can be talked to
                else if(hit.transform.gameObject.GetComponent<INpc>() != null)
                {
                    INpc npc = hit.transform.gameObject.GetComponent<INpc>();

                    //Throw up the prompt to talk to that specific NPC
                    interactionPrompt.promptTalk(npc.Name);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        beginConversation(npc);    
                    }
                }
            }
            //Otherwise, remove the interaction prompt from the screen
            //TODO: I would like to change this so that it isn't calling this more than necessary
            else
            {
                interactionPrompt.removePrompt();
            }
        }

        //Update called whenever the player has an item currently equipped
        private void equippedUpdate()
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                equipment.mainHand.GetComponent<IWeapon<float>>().Attack(20);
            }
            if (equipment.mainHand.GetComponent<IRangedWeapon<float>>() != null)
            { if (Input.GetKeyDown(KeyCode.R))
                {
                    equipment.mainHand.GetComponent<IRangedWeapon<float>>().Reload();
                }
            }
        }

        //Opens the player menu, disables character movement, displays mouse
        private void TogglePlayerMenu(bool newInMenu)
        {
            fpsController.inMenu = inMenu = newInMenu;
            CanvasGroup playerMenuCanvasGroup = playerMenuCanvas.GetComponent<CanvasGroup>();
            if (inMenu)
            {
                Cursor.lockState = CursorLockMode.Confined;
                playerMenuCanvasGroup.alpha = 1;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                playerMenuCanvasGroup.alpha = 0;
            }
            Cursor.visible = inMenu;
            playerMenuCanvasGroup.interactable = inMenu;
            playerMenuCanvasGroup.blocksRaycasts = inMenu;
        }

        private void beginConversation(INpc conversationPartner)
        {
            fpsController.inMenu = inMenu = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            conversationController.setConversationPartner(conversationPartner);
            conversationController.toggleConversationCanvas(true);
            interactionPrompt.removePrompt();
        }

        public void exitConversation()
        {
            fpsController.inMenu = inMenu = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            conversationController.toggleConversationCanvas(false);
        }

        private void endConversation()
        {

        }
        #endregion
    }
}
