using Character.PlayerCharacter;
using Character.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class RifleExample : MonoBehaviour, IRangedWeapon
{
    #region Member Declarations
    private FirstPersonController firstPersonController;
    private ICharacter<float> characterG;
    private float _defaultYSens;
    public float DefaultYSens
    {
        get => _defaultYSens;
        set => _defaultYSens = value;
    }
    private float _defaultXSens;
    public float DefaultXSens
    {
        get => _defaultXSens;
        set => _defaultXSens = value;
    }
    private float _dfov;
    public float DefaultFOV
    {
        get => _dfov;
        set => _dfov = value;
    }

    private float _gfov;
    public float GoalFOV
    {
        get => _gfov;
        set => _gfov = value;
    }
    private bool _equippable = true;
    public bool Equippable
    {
        get => _equippable;
    }

    [SerializeField] private string _weaponId = "rWeapEx";
    public string ID
    {
        get => _weaponId;
    }

    [SerializeField] private string _weaponName = "Example Ranged Weapon";
    public string Name
    {
        get => _weaponName;
    }

    [SerializeField] private string _flavourText = "Example implementation of a ranged weapon. Used to create a workflow for future weapons";
    public string FlavourText
    {
        get => _flavourText;
        set => _flavourText = value;
    }

    [SerializeField] private float _damage = 10;
    public float BaseDamage
    {
        get => _damage;
        set => _damage = value;
    }

    [SerializeField] private float _range = 50;
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

    [SerializeField] private float _weight = 20;
    public float Weight
    {
        get => _weight;
        set => _weight = value;
    }

    [SerializeField] private int _value = 100;
    public int MonetaryValue
    {
        get => _value;
        set => _value = value;
    }

    [SerializeField] private int _spaces = 3;
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

    //Whether the rifle should be ADS'ing
    private bool tADS = false;
    //Whether the rifle is ADS'ing
    private bool _ads = false;
    public bool ADS
    {
        get => _ads;
        set => _ads = value;
    }

    private bool _holster = false;
    public bool Holster
    {
        get => _holster;
        set => _holster = value;
    }

    private float _gunKickAcc = 0;
    public float GunKickAcc
    {
        get => _gunKickAcc;
        set => _gunKickAcc = value;
    }

    //Item Stats
    [SerializeField] private float _holsterSpeed = 3f;
    public float HolsterSpeed
    {
        get => _holsterSpeed;
    }

    [SerializeField] private float _minHolsterSpeed = 1f;
    public float MinHolsterSpeed
    {
        get => _minHolsterSpeed;
    }

    private float _holsterSpeedModifier = 1;
    public float HolsterSpeedModifier
    {
        get => _holsterSpeedModifier;
        set => _holsterSpeedModifier = value;
    }

    [SerializeField] private float _accuracy = 5f;
    public float Accuracy
    {
        get => _accuracy;
    }

    [SerializeField] private float _minAccuracy = 1f;
    public float MinAccuracy
    {
        get => _minAccuracy;
    }

    private float _accuracyModifier = 1;
    public float AccuracyModifier
    {
        get => _accuracyModifier;
        set => _accuracyModifier = value;
    }

    [SerializeField] private float _adsAccuracyModifier = 0.5f;
    public float ADSAccuracyModifier
    {
        get => _adsAccuracyModifier;
    }

    private float _adsAccuracyModifierModifier = 1;
    public float ADSAccuracyModifierModifier
    {
        get => _adsAccuracyModifierModifier;
        set => _adsAccuracyModifierModifier = value;
    }
    [SerializeField] private float _adsSpeed = 0.75f;
    public float ADSSpeed
    {
        get => _adsSpeed;
    }

    [SerializeField] private float _minADSSpeed = 0.1f;
    public float MinADSSpeed
    {
        get => _minADSSpeed;
    }

    [SerializeField] private float _gunKick = 1f;
    public float GunKick
    {
        get => _gunKick;
    }

    [SerializeField] private float _minGunKick = .1f;
    public float MinGunKick
    {
        get => _minGunKick;
    }

    private float _gunKickModifier = 1;
    public float GunKickModifier
    {
        get => _gunKickModifier;
        set => _gunKickModifier = value;
    }

    [SerializeField] private float _reloadSpeed = 6f;
    public float ReloadSpeed
    {
        get => _reloadSpeed;
    }

    [SerializeField] private float _minReloadSpeed = 2f;
    public float MinReloadSpeed
    {
        get => _minReloadSpeed;
    }

    private float _reloadSpeedModifier = 1;
    public float ReloadSpeedModifier
    {
        get => _reloadSpeedModifier;
        set => _reloadSpeedModifier = value;
    }

    [SerializeField] private int _magazineSize = 10;
    public int MagazineSize
    {
        get => _magazineSize;
    }
    private int _magazineSizeModifier = 0;
    public int MagazineSizeModifier
    {
        get => _magazineSizeModifier;
        set => _magazineSizeModifier = value;
    }

    [SerializeField] private float _zoom = 1.5f;
    public float Zoom
    {
        get => _zoom;
        set => _zoom = value;
    }

    private float _zoomModifier = 0;
    public float ZoomModifier
    {
        get => _zoomModifier;
        set => _zoomModifier = value;
    }

    [SerializeField] private float _timeToFire = 0.5f;
    public float TimeToFire
    {
        get => _timeToFire;
    }
    #endregion


    [SerializeField] public GameObject BulletHole;

    public void Start()
    {
        DefaultFOV = Camera.main.fieldOfView;
        GoalFOV = DefaultFOV;
        //TODO: Revisit this
        firstPersonController = GameObject.Find("Character Test").GetComponent<FirstPersonController>();
        DefaultXSens = firstPersonController.getXSensitivity();
        DefaultYSens = firstPersonController.getYSensitivity();
    }
    void Update()
    {
        if (anim.IsPlaying("RifleADS"))
        {
            float fullZoom = DefaultFOV / Zoom;
            Camera.main.fieldOfView = Mathf.Lerp(DefaultFOV, fullZoom, anim["RifleADS"].normalizedTime);
        }
        if ((tADS && !ADS) || (!tADS && ADS))
        {
            AimDownSight(characterG);
        }
        if (Holster && !anim.isPlaying)
        {
            gameObject.SetActive(false);
        }
        if (GunKickAcc > 0)
        {
            GunKickAcc -= (1f + Mathf.Sqrt(GunKickAcc)) * Time.deltaTime;
            if (GunKickAcc < 0)
            {
                GunKickAcc = 0;
            }
            Debug.Log(GunKickAcc);
        }
    }

    void IWeapon.Attack(ICharacter<float> character)
    {
        if (!rifleBusy())
        {
            if (AmmoCount != 0)
            {
                //Calculate the modifier to apply onto weapons accuracy
                float modifiedAccuracy = Mathf.Lerp(Accuracy, MinAccuracy, character.getRifleSkillModifier()) * AccuracyModifier;
                modifiedAccuracy += GunKickAcc;
                if (ADS)
                {
                    modifiedAccuracy *= ADSAccuracyModifier * ADSAccuracyModifierModifier;
                }
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
                        if (character is PlayerCharacter)
                        {
                            PlayerCharacter currentAsPlayerCharacter = character as PlayerCharacter;
                            Debug.Log("Rifle skill is now " + currentAsPlayerCharacter.increaseRifleSkill(2));
                        }
                    }
                    if (hit.transform.gameObject.layer == 11)
                    {
                        GameObject newHole = Instantiate(BulletHole, hit.point + Vector3.Scale(hit.normal, new Vector3(0.0001f, 0.0001f, 0.0001f)), Quaternion.FromToRotation(Vector3.up, hit.normal));
                        Destroy(newHole, 60f);
                    }
                }
                GunKickAcc += Mathf.Lerp(GunKick, MinGunKick, character.getRifleSkillModifier()) * GunKickModifier;
                anim["RifleFire"].speed = 1 / TimeToFire;
                anim.Play("RifleFire");
            }
            else
            {
                Debug.Log("out of ammo");
            }
        }
    }

    public void toggleADS(ICharacter<float> character)
    {
        characterG = character;
        tADS = !tADS;
    }

    private void AimDownSight(ICharacter<float> character)
    {
        if (!rifleBusy())
        {
            float timeToADS = Mathf.Lerp(ADSSpeed, MinADSSpeed, character.getRifleSkillModifier());
            if (ADS)
            {
                firstPersonController.setXSensitivity(DefaultXSens);
                firstPersonController.setYSensitivity(DefaultYSens);
                GoalFOV = DefaultFOV;
                if (!anim.IsPlaying("RifleADS"))
                {
                    anim["RifleADS"].normalizedTime = 1;
                }
                anim["RifleADS"].speed = -1 / timeToADS;
                anim.Play("RifleADS");
                ADS = false;
            }
            else
            {
                firstPersonController.setXSensitivity(DefaultXSens / Zoom);
                firstPersonController.setYSensitivity(DefaultYSens / Zoom);
                GoalFOV = Camera.main.fieldOfView / Zoom;
                anim["RifleADS"].speed = 1 / timeToADS;
                anim.Play("RifleADS");
                ADS = true;
            }
        }
    }

    public void HolsterWeapon(ICharacter<float> character)
    {
        //TODO: Maybe holstering should force an un-ADS but the code could be spaghetti with current implementations
        if (!anim.isPlaying && !ADS)
        {
            float timeToHolster = Mathf.Lerp(HolsterSpeed, MinHolsterSpeed, character.getRifleSkillModifier());
            if (Holster)
            {
                if (!anim.IsPlaying("RifleHolster"))
                {
                    anim["RifleHolster"].normalizedTime = 1;
                }
                anim["RifleHolster"].speed = -1 / timeToHolster;
                anim.Play("RifleHolster");
                Holster = false;
            }
            else
            {
                anim["RifleHolster"].speed = 1 / timeToHolster;
                anim.Play("RifleHolster");
                Holster = true;
            }
        }
    }

    void IWeapon.Break()
    {

    }

    void IWeapon.Repair()
    {

    }
    void IRangedWeapon.Reload(int numToReload, ICharacter<float> character)
    {
        //TODO: Maybe reloading should force an un-ADS but the code could be spaghetti with current implementations
        if (!rifleBusy() && !ADS)
        {
            float timeToReload = Mathf.Lerp(ReloadSpeed, MinReloadSpeed, character.getRifleSkillModifier()) * ReloadSpeedModifier;
            anim["RifleReload"].speed = 1 / timeToReload;
            anim.Play("RifleReload");
            AmmoCount = numToReload;
        }
    }

    bool rifleBusy()
    {
        if (anim.isPlaying || Holster)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
