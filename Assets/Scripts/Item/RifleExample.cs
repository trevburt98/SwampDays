using Character.PlayerCharacter;
using Character.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleExample : MonoBehaviour, IRangedWeapon
{
    #region Member Declarations
    private bool _equippable = true;
    public bool Equippable
    {
        get => _equippable;
    }

    private string _weaponId = "rWeapEx";
    public string ID
    {
        get => _weaponId;
    }

    private string _weaponName = "Example Ranged Weapon";
    public string Name
    {
        get => _weaponName;
    }

    private string _flavourText = "Example implementation of a ranged weapon. Used to create a workflow for future weapons";
    public string FlavourText
    {
        get => _flavourText;
        set => _flavourText = value;
    }

    private float _damage = 10;
    public float BaseDamage
    {
        get => _damage;
        set => _damage = value;
    }

    private float _range = 50;
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

    private List<int> _tags = new List<int>(){0};
    public List<int> Tags{
        get => _tags;
    }
    
    private int _maxStack = 1;
    public int MaxStack
    {
        get => _maxStack;
    }

    private int _currentStack = 1;
    public int NumInStack
    {
        get => _currentStack;
        set => _currentStack = value;
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

    private string[] _compatibleBulletArray = { "rifleAmmoEx" };
    public string[] BulletIDs
    {
        get => _compatibleBulletArray;
        set => _compatibleBulletArray = value;
    }

    private int _ammoCount = 10;
    public int AmmoCount
    {
        get => _ammoCount;
        set => _ammoCount = value;
    }

    private int _currentAmmo = 0;
    public int CurrentAmmoType
    {
        get => _currentAmmo;
        set => _currentAmmo = value;
    }

    private bool _ads;
    public bool ADS
    {
        get => _ads;
        set => _ads = value;
    }

    //Item Stats
    private float _holsterSpeed = 3f;
    public float HolsterSpeed{
        get => _holsterSpeed;
    }

    private float _minHolsterSpeed = 1f;
    public float MinHolsterSpeed{
        get => _minHolsterSpeed;
    }

    private float _holsterSpeedModifier = 0;
    public float HolsterSpeedModifier{
        get => _holsterSpeedModifier;
        set => _holsterSpeedModifier = value;
    }

    private float _accuracy = 2f;
    public float Accuracy
    {
        get => _accuracy;
    }

    private float _minAccuracy = 1f;
    public float MinAccuracy{
        get => _minAccuracy;
    }

    private float _accuracyModifier = 0;
    public float AccuracyModifier{
        get => _accuracyModifier;
        set => _accuracyModifier = value;
    }

    private float _adsSpeed = 1f;
    public float ADSSpeed{
        get => _adsSpeed;
    }

    private float _minADSSpeed = 0.1f;
    public float MinADSSpeed{
        get => _minADSSpeed;
    }

    private float _gunKick = 5f;
    public float GunKick{
        get => _gunKick;
    }

    private float _minGunKick = 1f;
    public float MinGunKick{
        get => _minGunKick;
    }

    private float _gunKickModifier = 0f;
    public float GunKickModifier{
        get => _gunKickModifier;
        set => _gunKickModifier = value;
    }

    private float _reloadSpeed = 6f;
    public float ReloadSpeed{
        get => _reloadSpeed;
    }

    private float _minReloadSpeed = 2f;
    public float MinReloadSpeed{
        get => _minReloadSpeed;
    }

    private float _reloadSpeedModifier = 0;
    public float ReloadSpeedModifier{
        get => _reloadSpeedModifier;
        set => _reloadSpeedModifier = value;
    }

    private int _magazineSize = 10;
    public int MagazineSize
    {
        get => _magazineSize;
    }

    private int _magazineSizeModifier = 0;
    public int MagazineSizeModifier{
        get => _magazineSizeModifier;
        set => _magazineSizeModifier = value;
    }

    [SerializeField] private float _zoom = 2f;
    public float Zoom
    {
        get => _zoom;
        set => _zoom = value;
    }

    private float _zoomModifier = 0;
    public float ZoomModifier{
        get => _zoomModifier;
        set => _zoomModifier = value;
    }
    #endregion


    public GameObject BulletHole;
    public GameObject newHole;
    
    public void Start()
    {

    }


    void IWeapon.Attack(ICharacter<float> character)
    {
        if(AmmoCount != 0)
        {
            //Calculate the modifier to apply onto weapons accuracy
            float accuracyModifier = character.getRifleSkillModifier();
            float modifiedAccuracy = Accuracy - ((Accuracy - MinAccuracy) * accuracyModifier);
            //Get the empty game object that represents where the bullet is fired from
            Transform startObject = transform.GetChild(0);
            RaycastHit hit;
            //Calculate the random x and y offsets to simulate sway
            float xOffset = Random.Range(-modifiedAccuracy, modifiedAccuracy);
            float yOffset = Random.Range(-modifiedAccuracy, modifiedAccuracy);
            //Get the direction from the x and y offsets
            Vector3 direction = new Vector3(xOffset, yOffset, Range);
            Vector3 fwd = startObject.TransformDirection(direction);
            Vector3 start = startObject.position;
            Debug.DrawRay(start, fwd, Color.red, 5.0f);
            //Play the sound for this particular weapon
            audioSource.PlayOneShot(Sound, 0.1f);
            Debug.Log("pew pew");

            //Update AmmoCount
            Debug.Log(AmmoCount);
            AmmoCount--;

            //Raycast hit something
            //TODO: If we want to do projectile physics this will change heavily
            if (Physics.Raycast(start, fwd, out hit, Range))
            {
                //Check the hit's layer to determine what course of action to take
                //Hit layer 9: NPC. Call the NPC's damage function
                if (hit.transform.gameObject.layer == 9)
                {
                    hit.transform.GetComponent<INpc>().Damage(BaseDamage);
                    //On character hit and if the character hitting is the player character, increase player character's appropriate skill
                    if(character is PlayerCharacter)
                    {
                        PlayerCharacter currentAsPlayerCharacter = character as PlayerCharacter;
                        Debug.Log("Rifle skill is now " + currentAsPlayerCharacter.increaseRifleSkill(2));
                    }
                }
                if(hit.transform.gameObject.layer == 11)
                {
                    newHole = Instantiate(BulletHole, hit.point + Vector3.Scale(hit.normal, new Vector3(0.0001f, 0.0001f, 0.0001f)), Quaternion.FromToRotation(Vector3.up, hit.normal));
                    Destroy(newHole, 60f);
                }
            }
        }
        else
        {
            Debug.Log("out of ammo");
        }
    }

    void IRangedWeapon.AimDownSight(ICharacter<float> character)
    {
        float timeToADS = ADSSpeed - ((ADSSpeed - MinADSSpeed) * character.getRifleSkillModifier());
        Transform hand = transform.parent;
        if (ADS){
            //TODO: change this to revert to a global or "default" fov value, probably chosen in settings by player
            Camera.main.fieldOfView = Camera.main.fieldOfView * Zoom;
            anim["RifleADS"].normalizedTime = 1;
            anim["RifleADS"].speed = -1 / timeToADS;
            anim.CrossFade("RifleADS");
            ADS = false;
        }
        else
        {
            //TODO change this to be "default" fov value over zoom instead of current value
            Camera.main.fieldOfView = Camera.main.fieldOfView / Zoom;
            anim["RifleADS"].time = 0;
            anim["RifleADS"].speed = 1 / timeToADS;
            anim.CrossFade("RifleADS");
            ADS = true;
        }
        
        
    }

    void IWeapon.Break()
    {

    }

    void IWeapon.Repair()
    {

    }

    void IRangedWeapon.Reload(int numToReload)
    {
        AmmoCount = numToReload;
    }
}
