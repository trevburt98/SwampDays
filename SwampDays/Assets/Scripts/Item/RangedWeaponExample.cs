using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponExample : MonoBehaviour, IRangedWeapon<float>
{
    //Inherited from IInteractable

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

    [SerializeField] private Sprite _weaponImage;
    public Sprite ItemImage
    {
        get => _weaponImage;
        set => _weaponImage = value;
    }

    //Inherited from IWeapon

    private float _damage = 10;
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

    //Inherited from IRangedWeapon

    private int[] _compatibleBulletArray;
    public int[] BulletIDs
    {
        get => _compatibleBulletArray;
        set => _compatibleBulletArray = value;
    }

    private float _accuracy = 2;
    public float Accuracy
    {
        get => _accuracy;
        set => _accuracy = value;
    }

    private int _magazineSize = 10;
    public int MagazineSize
    {
        get => _magazineSize;
        set => _magazineSize = value;
    }

    private int _ammoCount = 10;
    public int AmmoCount
    {
        get => _ammoCount;
        set => _ammoCount = value;
    }

    #endregion

    public GameObject BulletHole;
    public GameObject newhole;

    public void Start()
    {

    }


    void IWeapon<float>.Attack(float damageDone)
    {
        if (AmmoCount != 0)
        {
            Debug.Log(transform.position);
            Transform startObject = transform.GetChild(0);
            RaycastHit hit;

            // Vector3 fwd = startObject.TransformDirection(Vector3.forward) * 10;
            float xOffset = Random.Range(-Accuracy, Accuracy);
            float yOffset = Random.Range(-Accuracy, Accuracy);
            Vector3 direction = new Vector3(xOffset, yOffset, Range);
            Vector3 fwd = startObject.TransformDirection(direction);
            Vector3 start = startObject.position;

            //Play the sound for this particular weapon
            audioSource.PlayOneShot(Sound, 0.5f);
            Debug.Log("pew pew");

            //Update AmmoCount
            Debug.Log(AmmoCount);
            AmmoCount--;
            //Raycast hit something
            //If we want to do projectile physics this will change heavily
            if (Physics.Raycast(start, fwd, out hit, Range))
            {
                //Check the hit's layer to determine what course of action to take
                //Hit layer 9: NPC. Call the NPC's damage function
                if (hit.transform.gameObject.layer == 9)
                {
                    hit.transform.GetComponent<INpc>().Damage(BaseDamage);
                }
                Debug.Log(hit.point);
                if (hit.transform.gameObject.layer == 11)
                {
                    newhole = Instantiate(BulletHole, hit.point, Quaternion.FromToRotation(Vector3.up,hit.normal));
                    Destroy(newhole,3f);
                }
            }
        }
        else
        {
            Debug.Log("out of ammo");
        }
        
    }

    void IWeapon<float>.Break()
    {

    }

    void IWeapon<float>.Repair()
    {

    }

    void IRangedWeapon<float>.Reload()
    {
        AmmoCount = MagazineSize;
    }
}
