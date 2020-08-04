using System;
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

        private GameObject _bag;
        public GameObject Bag
        {
            get => _bag;
            set => _bag = value;
        }
        #endregion

        #region Skill Declarations

        private float _pistolSkill = 0;
        public float PistolSkill
        {
            get => _pistolSkill;
            set => _pistolSkill = value;
        }

        [SerializeField] private float _rifleSkill = 0;
        public float RifleSkill
        {
            get => _rifleSkill;
            set => _rifleSkill = value;
        }

        private float _heavyRifleSkill = 0;
        public float HeavyRifleSkill
        {
            get => _heavyRifleSkill;
            set => _heavyRifleSkill = value;
        }

        private float _shotgunSkill = 0;
        public float ShotgunSkill
        {
            get => _shotgunSkill;
            set => _shotgunSkill = value;
        }

        [SerializeField] private float _harvestSkill = 0;
        public float HarvestSkill
        {
            get => _harvestSkill;
            set => _harvestSkill = value;
        }

        #endregion

        public float maxHealth;
        public float currentHealth;

        public int armourRating;

        public EquipmentManager equipment;

        //Reference to the first person controller attached to the character
        [SerializeField] private FirstPersonController fpsController;
        //Reference to HUD UI canvas
        [SerializeField] private Canvas HUDCanvasUI;
        //Reference to Player Menu UI Canvas
        [SerializeField] private GameObject playerMenuCanvas;
        //Reference to the Player Menu Controller
        [SerializeField] private PlayerMenuController playerMenuController;
        //Reference to the camera of the character
        [SerializeField] private Camera camera;
        //Reference to the hand gameobject
        [SerializeField] private GameObject hand;
        [SerializeField] private AlchemyMenuController alchemyController;
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
                inMenu = !inMenu;
                TogglePlayerMenu(inMenu);
            }
            //if (Input.GetKeyDown(KeyCode.Tab) && !inMenu)
            //{
            //    TogglePlayerMenu(true);
            //}

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!inMenu)
                {
                    //TODO: Do a pause
                }
                else
                {
                    TogglePlayerMenu(false);
                    ToggleAlchemyMenu(false, null, -1);
                }

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

        #region Skill Incrementers
        public float increaseRifleSkill(float amountToIncrease)
        {
            RifleSkill += amountToIncrease;
            return RifleSkill;
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
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthUI.changeHealthUI(currentHealth);
        }

        public void ChangeCurrentHealth(float healthChange)
        {
            currentHealth += healthChange;
            if(currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            } else if(currentHealth <= 0)
            {
                Die();
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

        public void removeFromInventory(IItem item)
        {
            //bag.Remove(item);
            updateCarryingCapacity(-item.Weight);
        }

        //Simple update to indicate that stamina has been changed
        //TODO: prevent this from being called all the time, even when not needed, have to change implementation within fpscontroller
        public void updateStamina(float staminaChange)
        {
            Stamina += staminaChange;
            if (Stamina < 0)
            {
                Stamina = 0;
            }

            if (Stamina > maxStamina)
            {
                Stamina = maxStamina;
            }
            staminaUI.changeStaminaUI(Stamina);
        }

        
        //Change the amount that is currently being carried
        public void updateCarryingCapacity(float newCarryingCapacity)
        {
            int prevTier = currentTier;
            currentCarryingCapacity = newCarryingCapacity;

            //Zero check
            if (currentCarryingCapacity < 0)
            {
                currentCarryingCapacity = 0;
            }

            //Need to increase our current tier
            if (currentCarryingCapacity > tier[currentTier])
            {
                for (int i = currentTier; i < tier.Length; i++)
                {
                    if (currentCarryingCapacity <= tier[i] || i == 4)
                    {
                        currentTier = i;
                        break;
                    }
                }
            }
            //Need to decrease our current tier
            else if (currentTier > 0 && currentCarryingCapacity <= tier[currentTier - 1])
            {
                for (int i = currentTier; i >= 0; i--)
                {
                    if (currentCarryingCapacity >= tier[i])
                    {
                        currentTier = i;
                        break;
                    }
                }
            }

            //If we've changed tiers, apply the effects of the current tier to the player
            if (prevTier != currentTier)
            {
                applyCurrentTier();
            }
        }
        #endregion

        #region Private Functions
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
            if (i == 4)
            {
                fpsController.moveSpeedModifier = 0.75f;
                i--;
            }
            if (i == 3)
            {
                fpsController.runSpeedModifier = 0.75f;
                i--;
            }
            if (i == 2)
            {
                fpsController.staminaUseModifier = 1.25f;
                i--;
            }
            if (i == 1)
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
                if (hit.transform.GetComponent<IInteractable>() != null)
                {
                    IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();
                    interactionPrompt.promptPickup(interactable.Name);
                    try
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            hit.transform.GetComponent<IInteractable>().Interact(gameObject);
                        }
                    }
                    catch (NullReferenceException e)
                    {
                        Debug.Log("no bag equipped");
                    }
                }
                //If the raycast instead hit an NPC that can be talked to
                else if (hit.transform.gameObject.GetComponent<INpc>() != null)
                {
                    INpc npc = hit.transform.gameObject.GetComponent<INpc>();

                    //Throw up the prompt to talk to that specific NPC
                    interactionPrompt.promptTalk(npc.Name);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        beginConversation(npc);
                    }
                }
                //TODO: Remove this when merging with the IInteractable changes
                else if (hit.transform.gameObject.GetComponent<AlchemyTable>() != null)
                {
                    interactionPrompt.displayPrompt("Do Alchemy");
                    AlchemyTable alchemyTable = hit.transform.gameObject.GetComponent<AlchemyTable>();
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        alchemyTable.Interact(this);
                    }
                //    

                //    //Throw up the prompt for this interaction
                //    interactionPrompt.promptPickup(item.Name);
                //    //If the player presses the interaction button
                //    try
                //    {
                //        if (Input.GetKeyDown(KeyCode.E))
                //        {
                //            if (item is IBag && bag == null)
                //            {
                //                //IBag bag = interactable as IBag;
                //                //this.bag = bag;
                //                //Destroy(hit.transform.gameObject);
                //                hit.transform.gameObject.SetActive(false);
                //                hit.transform.parent = gameObject.transform;
                //                bag = hit.transform.gameObject;
                //            }
                //            else if (bag.GetComponent<IBag>().Add(hit.transform.gameObject))
                //            {
                //                updateCarryingCapacity(item.Weight);
                //                //Destroy(hit.transform.gameObject);
                //            }
                //        }
                //    }
                //    catch (NullReferenceException e)
                //    {
                //        Debug.Log("no bag equipped");
                //    }
                //}
                ////If the raycast instead hit an NPC that can be talked to
                //else if(hit.transform.gameObject.GetComponent<INpc>() != null)
                //{
                //    INpc npc = hit.transform.gameObject.GetComponent<INpc>();

                //    //Throw up the prompt to talk to that specific NPC
                //    interactionPrompt.promptTalk(npc.Name);
                //    if(Input.GetKeyDown(KeyCode.E))
                //    {
                //        beginConversation(npc);    
                //    }
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
            GameObject currentlyEquipped = equipment.mainHand;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                equipment.mainHand.GetComponent<IWeapon>().Attack(this);
            }

            if (currentlyEquipped.GetComponent<IRangedWeapon>() != null)
            {
                IRangedWeapon rangedWeapon = currentlyEquipped.GetComponent<IRangedWeapon>();
                IBag bagInventory = Bag.GetComponent<IBag>();
                if (Input.GetKeyDown(KeyCode.R))
                {
                    //Search our current bag for the ammo that our weapon currently has equipped
                    GameObject ammoObj = bagInventory.Find(rangedWeapon.BulletIDs[rangedWeapon.CurrentAmmoType]);
                    try
                    {
                        IAmmo ammo = ammoObj.GetComponent<IAmmo>();
                        //If we find the ammo that we are looking for
                        if (ammo.ID != null)
                        {
                            //If there is more than enough ammo in the bag to reload the gun to full
                            if (ammo.NumInStack > rangedWeapon.MagazineSize)
                            {
                                //Reload to full
                                rangedWeapon.Reload(rangedWeapon.MagazineSize, this);
                                //Decrement the number in the ammo stack
                                ammo.NumInStack -= rangedWeapon.MagazineSize;
                                //Free up those bag spaces
                                bagInventory.CurrentSpaces -= (int)(ammo.Weight * rangedWeapon.MagazineSize);
                            }
                            //If we either don't have enough to reload the gun to full or can reload to exactly full
                            else
                            {
                                //Reload the gun with all the ammo left in the stack
                                rangedWeapon.Reload(ammo.NumInStack, this);
                                //Remove the ammo from the bag
                                bagInventory.Remove(ammoObj, transform.gameObject);
                            }
                        }
                    }
                    catch (NullReferenceException e)
                    {
                        Debug.Log("don't have the right ammo");
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                equipment.mainHand.SetActive(true);
                equipment.mainHand.GetComponent<IRangedWeapon>().HolsterWeapon(this);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                equipment.mainHand.GetComponent<IRangedWeapon>().toggleADS(this);
            }
            //add check for if ads is set to hold instead of toggle
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                equipment.mainHand.GetComponent<IRangedWeapon>().toggleADS(this);
            }
        }

        //Opens the player menu, disables character movement, displays mouse
        private void TogglePlayerMenu(bool newInMenu)
        {
            if (newInMenu != playerMenuCanvas.activeInHierarchy)
            {
                fpsController.inMenu = inMenu = newInMenu;
                playerMenuCanvas.gameObject.SetActive(inMenu);
                Cursor.visible = inMenu;
                if (inMenu)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    playerMenuController.openMenu();
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }

        public void ToggleAlchemyMenu(bool newInMenu, List<AlchemyBase> baseList, int numIngredients)
        {
            if (newInMenu != alchemyController.gameObject.activeInHierarchy)
            {
                fpsController.inMenu = inMenu = newInMenu;
                alchemyController.ToggleAlchemyMenu(newInMenu, baseList, numIngredients);
                Cursor.visible = inMenu;
                if (inMenu)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }

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
