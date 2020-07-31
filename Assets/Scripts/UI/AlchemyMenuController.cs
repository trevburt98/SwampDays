using Character.PlayerCharacter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyMenuController : MonoBehaviour
{
    public GameObject selectedIngredientButton;

    public GameObject basePanel;
    public GameObject baseButtonPrefab;

    public GameObject selectedPanel;
    public GameObject inventoryPanel;
    public GameObject ingredientButton;

    public PlayerCharacter player;

    private AlchemyBase selectedBase;

    private Button currentButton;
    List<GameObject> currentButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleAlchemyMenu(bool active, List<AlchemyBase> baseList, int numIngredients)
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

            for(int i = 0; i < numIngredients; i++)
            {
                GameObject newObj = GameObject.Instantiate(selectedIngredientButton);
                newObj.transform.parent = selectedPanel.transform;
                currentButtons.Add(newObj);
            }

            PopulateApplicableInventory();
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
        foreach(Transform trans in player.Bag.transform)
        {
            IItem item = trans.gameObject.GetComponent<IItem>();
            foreach(int tag in item.Tags)
            {
                if (tag == 3)
                {
                    GameObject newObj = GameObject.Instantiate(ingredientButton);
                    newObj.transform.parent = inventoryPanel.transform;
                    newObj.GetComponentInChildren<Text>().text = item.Name;
                    newObj.GetComponentInChildren<Button>().onClick.AddListener(delegate { SetSelectedIngredient(item); });
                }
            }
        }
    }

    private void SetSelectedIngredient(IItem ingredient)
    {
        Debug.Log("setting " + ingredient.Name);
    }
}
