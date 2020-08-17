using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Character.PlayerCharacter;

public class SampleCombatant : MonoBehaviour, INpc
{
    [SerializeField] private List<Transform> patrolLocations;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform eyes;
    [SerializeField] private Transform player;
    [SerializeField] private float minIdleTime;
    [SerializeField] private float maxIdleTime;
    [SerializeField] private List<AudioClip> perkSounds;
    [SerializeField] private List<AudioClip> investigateSounds;
    [SerializeField] private AudioSource audioSource;
    private enum CombatState
    {
        Posted,
        Patrolling,
        Perked,
        Investigating,
        Paranoid,
        Alerted,
        Searching,
        Relaxing,
        seekingCover,
        inCover,
        Advancing,
        Retreating,
        Fleeing
    }
    private static System.Random random = new System.Random();
    private CombatState state = CombatState.Posted;
    private float timeSinceUpdate = 0;
    private float timeToUpdate = 0;
    private bool becameIdle = false;
    //TODO: player detection stats moved onto INpc?
    private float visionAngle = 30;
    private float visionRange = 20;
    private float partialVisionAngle = 60;
    private float partialVisionRange = 50;
    private float personalSpaceRadius = 2;
    private Vector3 lastKnownPlayerLocation;
    private float playerDetectionAccumulator = 0;
    private bool detectionThisFrame = false;

    #region Trait Declarations
    private string _name = "Sample Combatant";
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    private string _interactPrompt = "";
    public string InteractPrompt
    {
        get => _interactPrompt;
        set => _interactPrompt = value;
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
    private int currentCarryingCapacity;
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

    private List<ConversationLine> _conversationLines;
    public List<ConversationLine> ConversationLines
    {
        get => _conversationLines;
    }

    private int _currentLinePtr;
    public int CurrentLinePtr
    {
        get => _currentLinePtr;
        set => CurrentLinePtr = value;
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

    private float _rifleSkill = 0;
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

    private float _harvestSkill = 0;
    public float HarvestSkill
    {
        get => _harvestSkill;
        set => _harvestSkill = value;
    }
    #endregion

    private float maxHealth;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = currentHealth = Vitality * 2;
    }
    void Update()
    {
        //Debug.Log(playerDetectionAccumulator);
        playerDetection();
        timeSinceUpdate += Time.deltaTime;
        switch (state)
        {
            case CombatState.Posted:
                if (playerDetectionAccumulator > 0)
                {
                    becomePerked();
                }
                else if (timeSinceUpdate >= timeToUpdate)
                {
                    becomePatrolling();
                }
                break;
            case CombatState.Patrolling:
                if (playerDetectionAccumulator > 0)
                {
                    becomePerked();
                }
                else if (!agent.hasPath)
                {
                    becomePosted();
                }
                break;
            case CombatState.Perked:
                if (playerDetectionAccumulator >= 33){
                    becomeInvestigating();
                }
                else if (!agent.hasPath){
                    becomeRelaxing();
                }
                break;
            case CombatState.Investigating:
                if (playerDetectionAccumulator >= 66){
                    becomeParanoid();
                }
                else if(!agent.hasPath){
                    becomeRelaxing();
                }
                break;
            case CombatState.Relaxing:
                if(playerDetectionAccumulator <= 0){
                    playerDetectionAccumulator = 0;
                    becomePatrolling();
                }
                else if (!detectionThisFrame){
                    //TODO: Some kind of looking around animation
                    playerDetectionAccumulator -= Time.deltaTime * 10;
                }
                else
                {
                    becomePerked();
                }
                break;
        }
    }
    public void Damage(float damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Heal(float healthHealed)
    {

    }

    public void ChangeCurrentHealth(float healthChange)
    {

    }

    public void Die()
    {
        Destroy(this.gameObject);
        //TODO: drop a lootable body?
    }
    public void addConversationLine(ConversationLine newLine) { }

    public int startConversation() { return 0; }

    public void endConversation() { }

    public void Interact(GameObject user) { }

    private void becomePosted()
    {
        state = CombatState.Posted;
        timeSinceUpdate = 0;
        timeToUpdate = (float)random.NextDouble() * maxIdleTime + minIdleTime;
    }

    private void becomePatrolling()
    {
        int r = random.Next(patrolLocations.Count);
        agent.SetDestination(patrolLocations[r].position);
        state = CombatState.Patrolling;
        timeSinceUpdate = 0;
    }

    private void becomePerked()
    {
        state = CombatState.Perked;
        Vector3 direction = Vector3.Normalize(lastKnownPlayerLocation -  eyes.position);
        agent.SetDestination(eyes.position + (direction * 3));
        Debug.Log(agent.path.corners.Length);
        if (agent.path.corners.Length > 2){
            agent.SetDestination(eyes.position + direction);
            if (agent.path.corners.Length > 2){
                agent.ResetPath();
            }
        }
        timeSinceUpdate = 0;
        //TODO: trigger vocal bark (e.g. "Huh?")
    }
    
    private void becomeInvestigating(){
        state = CombatState.Investigating;
        agent.SetDestination(lastKnownPlayerLocation);
        timeSinceUpdate = 0;
        //TODO: trigger vocal bark (e.g. "Better check it out...")
    }

    private void becomeParanoid(){

    }

    private void becomeRelaxing(){
        state = CombatState.Relaxing;
    }

    private void playerDetection()
    {
        detectionThisFrame = false;
        bool hasLineOfSight = false;
        RaycastHit hit;
        if(Physics.Raycast(eyes.position, player.position - eyes.position, out hit, partialVisionRange)){
            hasLineOfSight = (hit.transform.GetComponent<PlayerCharacter>() != null);
        }
        float playerDistance = Vector3.Distance(eyes.position, player.position);
        Vector3 fwd = eyes.forward * visionRange;
        Vector3 farFwd = eyes.forward * partialVisionRange;
        Vector3 toPlayer = player.position - eyes.position;
        float angle = Vector3.Angle(fwd, toPlayer);

        //TODO: Remove Debug Code
        Vector3 leftBound = Quaternion.Euler(0, visionAngle, 0) * fwd;
        Vector3 rightBound = Quaternion.Euler(0, -visionAngle, 0) * fwd;
        Vector3 farLeftBound = Quaternion.Euler(0, partialVisionAngle, 0) * farFwd;
        Vector3 farRightBound = Quaternion.Euler(0, -partialVisionAngle, 0) * farFwd;
        Debug.DrawLine(eyes.position, eyes.position + leftBound);
        Debug.DrawLine(eyes.position, eyes.position + rightBound);
        Debug.DrawLine(eyes.position, eyes.position + farLeftBound);
        Debug.DrawLine(eyes.position, eyes.position + farRightBound);
        //End of Debug Code

        if (hasLineOfSight && angle < visionAngle && playerDistance < visionRange)
        {
            playerDetectionAccumulator += 20 * Time.deltaTime;
            timeSinceUpdate = 0;
            //TODO: Jitter this a little bit
            lastKnownPlayerLocation = player.position;
            detectionThisFrame = true;
        }
        else if (hasLineOfSight && angle < partialVisionAngle && playerDistance < partialVisionRange)
        {
            playerDetectionAccumulator += 10 * Time.deltaTime;
            timeSinceUpdate = 0;
            //TODO: Jitter this a lotta bit
            lastKnownPlayerLocation = player.position;
            detectionThisFrame = true;
        }
        if (playerDetectionAccumulator > 100){
            playerDetectionAccumulator = 100;
        }
    }
}
