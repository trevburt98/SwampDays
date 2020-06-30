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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //TODO: I really don't like this system, I'd like to do something different instead
        List<Response> responseList = new List<Response>();
        responseList.Add(new Response("General Kenobi", 1));
        responseList.Add(new Response("*Lightsaber whoosing noise*", 2));

        ConversationLines.Add(new ConversationLine("Hello There!", responseList));
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

    public void Die()
    {

    }
}
