using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Character.PlayerCharacter;
using Character.Extensions;

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
    [SerializeField] private List<AudioClip> paranoidSounds;
    [SerializeField] private List<AudioClip> alertSounds;
    [SerializeField] private List<AudioClip> relaxSounds;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform debugDestTarget;
    [SerializeField] private Transform debugLookTarget;
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
    private int _strength = 20;
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
    private float moveSpeed = 0;
    private Vector3 lookAtTarget;
    private Vector3 currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        setLookingTarget(eyes.position + eyes.forward);
        agent.updateRotation = false;
        maxHealth = currentHealth = Vitality * 2;
        moveSpeed = this.calculateMoveSpeed();
        toggleUrgency(false);
    }
    void Update()
    {
        //Debug.Log(playerDetectionAccumulator);
        playerDetection();
        timeSinceUpdate += Time.deltaTime;
        switch (state)
        {
            case CombatState.Posted:
            setLookingTarget(eyes.position + eyes.forward);
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
            setLookingTarget(agent.steeringTarget);
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
            setLookingTarget(lastKnownPlayerLocation);
                if (playerDetectionAccumulator >= 33)
                {
                    becomeInvestigating();
                }
                else if (!agent.hasPath)
                {
                    becomeRelaxing();
                }
                break;
            case CombatState.Investigating:
            setLookingTarget(lastKnownPlayerLocation);
                if (playerDetectionAccumulator >= 66)
                {
                    becomeParanoid();
                }
                else if (!agent.hasPath)
                {
                    becomeRelaxing();
                }
                break;
            case CombatState.Paranoid:
            setLookingTarget(lastKnownPlayerLocation);
                if (playerDetectionAccumulator >= 100)
                {
                    becomeAlerted();
                }
                else if (detectionThisFrame)
                {
                    becomeParanoid();
                }
                else if (!agent.hasPath)
                {
                    becomeSearching();
                }
                break;
            case CombatState.Alerted:
            setLookingTarget(lastKnownPlayerLocation);
                if (detectionThisFrame)
                {
                    becomeAlerted();
                }
                else if (!agent.hasPath)
                {
                    becomeSearching();
                }
                break;
            case CombatState.Searching:
            setLookingTarget(agent.steeringTarget);
                if (detectionThisFrame)
                {
                    becomeParanoid();
                }
                if (!agent.hasPath)
                {
                    becomeSearching();
                }
                break;
            case CombatState.Relaxing:
            setLookingTarget(eyes.position + eyes.forward);
                if (playerDetectionAccumulator <= 0)
                {
                    playerDetectionAccumulator = 0;
                    becomePatrolling();
                }
                else if (!detectionThisFrame)
                {
                    //TODO: Some kind of looking around animation
                    playerDetectionAccumulator -= Time.deltaTime * 10;
                }
                else
                {
                    becomePerked();
                }
                break;
        }
        calculateLookAngle();
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
        Debug.Log("Standin' around...");
    }

    private void becomePatrolling()
    {
        int r = random.Next(patrolLocations.Count);
        setWalkingTarget(patrolLocations[r].position);
        state = CombatState.Patrolling;
        timeSinceUpdate = 0;
        Debug.Log("Patrollin' around...");
    }

    private void becomePerked()
    {
        state = CombatState.Perked;
        Vector3 direction = Vector3.Normalize(lastKnownPlayerLocation - eyes.position);
        setWalkingTarget(eyes.position + (direction * 3));
        if (agent.path.corners.Length > 2)
        {
            setWalkingTarget(eyes.position + direction);
            if (agent.path.corners.Length > 2)
            {
                agent.ResetPath();
            }
        }
        timeSinceUpdate = 0;
        //TODO: trigger vocal bark (e.g. "Huh?")
        Debug.Log("Did I see something?");
    }

    private void becomeInvestigating()
    {
        state = CombatState.Investigating;
        setWalkingTarget(getChaseLocation(lastKnownPlayerLocation));
        timeSinceUpdate = 0;
        //TODO: trigger vocal bark (e.g. "Better check it out...")
        Debug.Log("Lemme go check that out...");
    }

    private void becomeParanoid()
    {
        state = CombatState.Paranoid;
        //TODO: Alert Nearby Enemies, giving them 66 detection and causing them to become searching
        setWalkingTarget(getChaseLocation(lastKnownPlayerLocation));
        timeSinceUpdate = 0;
        toggleUrgency(true);
        Debug.Log("I think there's someone here");
    }

    private void becomeAlerted()
    {
        state = CombatState.Alerted;
        //TODO: Alert Nearby Enemies, giving them 100 detection, accurately updating their lastKnownPlayerLocation, and causing them to become Alerted.
        setWalkingTarget(getChaseLocation(lastKnownPlayerLocation));
        timeSinceUpdate = 0;
        toggleUrgency(true);
        Debug.Log("I found him!");
    }

    private void becomeSearching()
    {
        state = CombatState.Searching;
        //TODO: determine search desination some toher way?
        int r = random.Next(patrolLocations.Count);
        setWalkingTarget(patrolLocations[r].position);
        timeSinceUpdate = 0;
        toggleUrgency(false);
        Debug.Log("I lost him!");
    }

    private void becomeRelaxing()
    {
        state = CombatState.Relaxing;
        timeSinceUpdate = 0;
        Debug.Log("Guess it was nothing...");
    }

    private void toggleUrgency(bool urgent)
    {
        if (urgent)
        {
            agent.speed = moveSpeed * 2;
            Debug.Log("Go go go!");
        }
        else
        {
            agent.speed = moveSpeed;
        }
    }

    private Vector3 getChaseLocation(Vector3 playerLocation)
    {
        float playerDistance = Vector3.Distance(eyes.position, playerLocation);
        Vector3 directionToPlayer = Vector3.Normalize(playerLocation - eyes.position);
        return (eyes.position + (directionToPlayer * (playerDistance - 5)));
    }

    private void playerDetection()
    {
        detectionThisFrame = false;
        bool hasLineOfSight = false;
        RaycastHit hit;
        if (Physics.Raycast(eyes.position, player.position - eyes.position, out hit, partialVisionRange))
        {
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
        if (playerDetectionAccumulator > 100)
        {
            playerDetectionAccumulator = 100;
        }
    }

    private void setWalkingTarget(Vector3 target)
    {
        agent.SetDestination(target);
        //TODO: Debug code
        Vector3 debugLocation = new Vector3(target.x, debugDestTarget.position.y, target.z);
        debugDestTarget.position = debugLocation;
        //End debug code
    }

    private void setLookingTarget(Vector3 target)
    {
        lookAtTarget = target;
        //TODO: Debug code
        Vector3 debugLocation = new Vector3(target.x, debugLookTarget.position.y, target.z);
        debugLookTarget.position = debugLocation;
        //End debug code
    }

    private void calculateLookAngle()
    {
        Vector3 fwd = eyes.forward;
        Vector3 toTarget = lookAtTarget - eyes.position;
        float currentAngle = Vector3.SignedAngle(Vector3.forward, fwd, Vector3.up);
        float targetAngle = Vector3.SignedAngle(Vector3.forward, toTarget, Vector3.up);
        float neededTurn = targetAngle - currentAngle;
        float maxTurn = agent.angularSpeed * Time.deltaTime;
        if (neededTurn < 0)
        {
            maxTurn *= -1;
        }
        float turnAmount = Mathf.Abs(neededTurn) < Mathf.Abs(maxTurn) ? neededTurn : maxTurn;
        if(Vector3.Magnitude(toTarget) < 2){
            turnAmount = 0;
        }
        float newAngle = currentAngle + turnAmount;
        transform.rotation = Quaternion.Euler(transform.rotation.x, newAngle, transform.rotation.z);
    }
}
