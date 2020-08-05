using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : MonoBehaviour, INpc
{

    #region Trait Declarations
    private string _name = "Sample Enemy";
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

    public float maxHealth;
    public float currentHealth;

    [SerializeField] private QuestManager questManager;

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

    public void Heal(float healthHealed)
    {

    }

    public void ChangeCurrentHealth(float healthChange)
    {

    }

    public void Die()
    {
        Debug.Log("well shit, guess i'll die then");
        IQuest applicableQuest = questManager.questList[0];
        if(applicableQuest.Status == 1 || applicableQuest.Status == 2)
        {
            applicableQuest.completeQuest();
        }
        Destroy(this.gameObject);
    }

    public void addConversationLine(ConversationLine newLine)
    {
        
    }

    public int startConversation()
    {
        return 0;
    }

    public void endConversation()
    {
        
    }

    public void Interact(GameObject user)
    {

    }
}
