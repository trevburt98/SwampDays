using Character.PlayerCharacter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyMenuController : MonoBehaviour
{
    public GameObject basePanel;
    public GameObject baseButtonPrefab;

    public GameObject inventoryPanel;

    public PlayerCharacter player;

    private AlchemyBase selectedBase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleAlchemyMenu(bool active, List<AlchemyBase> baseList)
    {
        try
        {
            gameObject.SetActive(active);
            foreach (AlchemyBase alchemyBase in baseList)
            {
                GameObject newObj = GameObject.Instantiate(baseButtonPrefab);
                newObj.transform.parent = basePanel.transform;
                newObj.GetComponentInChildren<Text>().text = alchemyBase.BaseName;
                newObj.GetComponentInChildren<Button>().onClick.AddListener(delegate { SetSelectedBase(alchemyBase); });
            }
        } catch(NullReferenceException e)
        {
            Debug.Log("closing");
        }


    }

    private void SetSelectedBase(AlchemyBase newBase)
    {
        selectedBase = newBase;
    }

    private void PopulateApplicableInventory()
    {
        
    }
}
