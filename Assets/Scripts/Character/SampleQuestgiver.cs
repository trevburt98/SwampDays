﻿using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SampleQuestgiver : MonoBehaviour, INpc
{
    #region Trait Declarations
    private string _name = "Sample Questgiver";
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    private string _interactPrompt = "Talk to Sample Questgiver";
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

    private int _opinion = 100;
    public int Opinion
    {
        get => _opinion;
        set => _opinion = value;
    }

    private List<ConversationLine> _conversationLines = new List<ConversationLine>();
    public List<ConversationLine> ConversationLines
    {
        get => _conversationLines;
    }

    private int _currentLinePtr = 0;
    public int CurrentLinePtr
    {
        get => _currentLinePtr;
        set => _currentLinePtr = value;
    }

    private GameObject _bag;
    public GameObject Bag
    {
        get;
        set;
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

    [SerializeField] private QuestManager questManager;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: I really don't like this system, I'd like to do something different instead
        List<Response> responseList = new List<Response>();
        responseList.Add(new Response("General Kenobi", 1));
        responseList.Add(new Response("*Lightsaber whoosing noise*", 2));
        ConversationLines.Add(new ConversationLine("Hello There!", responseList));

        ConversationLines.Add(new ConversationLine("And then we fight", null));

        List<Response> questOfferResponses = new List<Response>();
        questOfferResponses.Add(new Response("Yeah, sure, why not", 3, 0));
        questOfferResponses.Add(new Response("Nah fam, I'm aight", 4));
        questOfferResponses.Add(new Response("I'll think about it", 5));
        ConversationLines.Add(new ConversationLine("Want to do this quest for me right quick", questOfferResponses));

        ConversationLines.Add(new ConversationLine("Alright then, here you go", null));

        ConversationLines.Add(new ConversationLine("Alright then, screw you too", null));

        ConversationLines.Add(new ConversationLine("Well let me know if you want to", null));

        List<Response> questCompleteResponses = new List<Response>();
        questCompleteResponses.Add(new Response("Yup, killed that boi dead", 7, 0));
        questCompleteResponses.Add(new Response("No, not yet", 8));
        ConversationLines.Add(new ConversationLine("You done with that quest yet?", questCompleteResponses));

        ConversationLines.Add(new ConversationLine("Naisu", null));

        ConversationLines.Add(new ConversationLine("Alright, get back to it then", null));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Damage(float damageDone)
    {

    }

    public void Heal(float healthHealed)
    {

    }

    public void ChangeCurrentHealth(float healthChange)
    {

    }

    public void Die()
    {

    }

    public void Interact(GameObject user)
    {
        PlayerCharacter player = user.GetComponent<PlayerCharacter>();
        player.beginConversation(this);
    }

    public int startConversation()
    {
        //Finished, not turned in yet
        if (questManager.questList[0].Status == 4)
        {
            CurrentLinePtr = 6;
        }
        return CurrentLinePtr;
    }

    public void endConversation()
    {

    }

    public void addConversationLine(ConversationLine newConversationLine)
    {

    }
}
