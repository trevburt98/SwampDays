using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordExample : MonoBehaviour, IMeleeWeapon
{
    #region Member Declarations

    private bool _equippable = true;
    public bool Equippable
    {
        get => _equippable;
    }

    private string _weaponId = "mWeapEx";
    public string ID
    {
        get => _weaponId;
    }

    private string _weaponName = "Example Melee Weapon";
    public string Name
    {
        get => _weaponName;
    }

    private string _flavourText = "Example implementation of a melee weapon. Used to create a workflow for future weapons";
    public string FlavourText
    {
        get => _flavourText;
        set => _flavourText = value;
    }

    private float _damage = 1;
    public float BaseDamage
    {
        get => _damage;
        set => _damage = value;
    }

    private float _range = 10;
    public float Range
    {
        get => _range;
        set => _range = value;
    }

    private float _durability;
    public float Durability
    {
        get => _durability;
        set => _durability = value;
    }

    private float _weight = 20;
    public float Weight
    {
        get => _weight;
        set => _weight = value;
    }

    private int _value = 100;
    public int MonetaryValue
    {
        get => _value;
        set => _value = value;
    }

    private int _spaces = 3;
    public int InventorySpaces
    {
        get => _spaces;
        set => _spaces = value;
    }

    private List<int> _tags = new List<int>() { 0 };
    public List<int> Tags
    {
        get => _tags;
    }

    [SerializeField] private Sprite _weaponImage;
    public Sprite ItemImage
    {
        get => _weaponImage;
        set => _weaponImage = value;
    }

    private bool _broken = false;
    public bool Broken
    {
        get => _broken;
        set => _broken = value;
    }

    [SerializeField] private AudioClip _fireSound;
    public AudioClip Sound
    {
        get => _fireSound;
        set => _fireSound = value;
    }
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Animation anim;

    //Item Stats
    private float _holsterSpeed = 3f;
    public float HolsterSpeed
    {
        get => _holsterSpeed;
    }

    private float _minHolsterSpeed = 1f;
    public float MinHolsterSpeed
    {
        get => _minHolsterSpeed;
    }

    private float _holsterSpeedModifier = 0;
    public float HolsterSpeedModifier
    {
        get => _holsterSpeedModifier;
        set => _holsterSpeedModifier = value;
    }

    private float _strengthMultiplier;
    public float StrengthMultiplier
    {
        get => _strengthMultiplier;
        set => _strengthMultiplier = value;
    }

    private float _blockDamageReduction;
    public float BlockDamageReduction
    {
        get => _blockDamageReduction;
        set => _blockDamageReduction = value;
    }

    private bool _blocking;
    public bool Blocking
    {
        get => _blocking;
        set => _blocking = value;
    }

    private int _maxStack;
    public int MaxStack
    {
        get => _maxStack;
    }

    private int _numInStack;
    public int NumInStack
    {
        get => _numInStack;
        set => _numInStack = value;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void IWeapon.Attack(ICharacter<float> character)
    {
        //Swing your mighty sword
        Transform hand = transform.parent;
        anim["Schwap"].time = 0;
        anim["Schwap"].speed = 5;
        anim.CrossFade("Schwap");
        Debug.Log("Gyaaaah!!!");
    }

    //Block (shamelessly copied from ADS)
    void IMeleeWeapon.Block(ICharacter<float> character)
    {
        Transform hand = transform.parent;
        if (Blocking)
        {
            anim["Block"].normalizedTime = 1;
            anim["Block"].speed = -1;
            anim.CrossFade("Block");
            Blocking = false;
        }
        else
        {
            anim["Block"].time = 0;
            anim["Block"].speed = 1;
            anim.CrossFade("Block");
            Blocking = true;
        }
    }

    void IMeleeWeapon.HeavyAttack(ICharacter<float> character)
    {
        //Swing your mighty sword
        Transform hand = transform.parent;
        anim["BigOlThonk"].time = 0;
        anim["BigOlThonk"].speed = 1;
        anim.CrossFade("BigOlThonk");
        Debug.Log("AAAAAAAAAA!!!");
    }

    void IWeapon.Break()
    {

    }

    void IWeapon.Repair()
    {

    }

    //Create collision between sword and meat
    void OnTriggerEnter(Collider weaponCollider)
    {
        if (anim.IsPlaying("Schwap"))
        {
            Debug.Log("hit");
            //Check the hit's layer to determine what course of action to take
            //Hit layer 9: NPC. Call the NPC's damage function
            if (weaponCollider.gameObject.layer == 9)
            {
                weaponCollider.gameObject.GetComponent<INpc>().Damage(BaseDamage);
                Debug.Log("harder daddy");
            }
        }

        if (anim.IsPlaying("BigOlThonk"))
        {
            Debug.Log("hit");
            //Check the hit's layer to determine what course of action to take
            //Hit layer 9: NPC. Call the NPC's damage function
            if (weaponCollider.gameObject.layer == 9)
            {
                weaponCollider.gameObject.GetComponent<INpc>().Damage(BaseDamage*20);
                Debug.Log("SWEET MERCIFUL JESUS! MAKE IT STOP!");
            }
        }
    }
}
