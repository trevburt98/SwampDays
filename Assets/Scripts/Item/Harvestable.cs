using System;
using System.Collections;
using System.Collections.Generic;
using Character.Extensions;
using UnityEngine;

public class Harvestable : MonoBehaviour, IInteractable
{
    public float abundance;
    public int minHarvest;
    public int maxHarvest;
    public int skillMax;
    //[SerializeField] public GameObject prefab;
    [Serializable]
    public struct prefabStruct
    {
        public GameObject prefab;
        public float chance;
    }

    public prefabStruct[] prefabList;
    public int defaultPrefab;


    private string _name = "Bush Example";
    public string Name
    {
        get => _name;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(GameObject user)
    {
        System.Random rand = new System.Random();
        float i1 = (float)rand.Next(minHarvest / 2, maxHarvest / 2);
        float i2 = (float)rand.Next(minHarvest / 2, maxHarvest / 2);

        float baseHarvest = i1 + i2;
        float skillHarvest = Mathf.Lerp(0, skillMax, user.GetComponent<ICharacter<float>>().getHarvestSkill());
        int harvest = (int)Math.Floor((baseHarvest + skillHarvest) * abundance);

        bool defaultInstantiated = false;
        //TODO: replace the repeated if statements but I don't really feel like doing that right now
        for(int i = 0; i < harvest; i++)
        {
            GameObject newObj;
            if(!defaultInstantiated)
            {
                newObj = GameObject.Instantiate(prefabList[defaultPrefab].prefab);
                if (!user.GetComponent<ICharacter<float>>().Bag.GetComponent<IBag>().Add(newObj, user))
                {
                    //We can change this to a drop if we want to
                    Destroy(newObj);
                }
                defaultInstantiated = true;
            } else
            {
                double random = rand.NextDouble();
                double current = 0;
                foreach(prefabStruct prefab in prefabList)
                {
                    if(random >= current && random <= current + prefab.chance)
                    {
                        newObj = GameObject.Instantiate(prefab.prefab);
                        if (!user.GetComponent<ICharacter<float>>().Bag.GetComponent<IBag>().Add(newObj, user))
                        {
                            //We can change this to a drop if we want to
                            Destroy(newObj);
                        }
                    }
                    current += prefab.chance;
                }
            }
        }
    }
}
