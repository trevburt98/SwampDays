using Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingMenuController : MonoBehaviour
{
    public PlayerCharacter player;

    public GameObject inventoryPanel;
    public GameObject ingredientButton;

    public GameObject selectedPanel;
    public GameObject selectedIngredientButton;

    List<GameObject> currentButtons;
    List<GameObject> currentIngredients;

    public RecipeDictionary recipeDictionary;

    int numIngredients;

    // Start is called before the first frame update
    void Start()
    {
        recipeDictionary = new RecipeDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleCraftingMenu(bool active, int numIngredients)
    {
        currentButtons = new List<GameObject>();
        currentIngredients = new List<GameObject>();

        this.numIngredients = numIngredients;

        gameObject.SetActive(active);
        PopulateCraftingMenu();
    }

    private void PopulateCraftingMenu()
    {
        ClearCraftingMenu();
        PopulateApplicableInventory();
        PopulateSelectedButtons();
    }

    private void PopulateApplicableInventory()
    {
        foreach (Transform trans in player.Bag.transform)
        {
            IItem item = trans.gameObject.GetComponent<IItem>();
            foreach (int tag in item.Tags)
            {
                if (tag == 12)
                {
                    GameObject newObj = GameObject.Instantiate(ingredientButton);
                    newObj.transform.parent = inventoryPanel.transform;
                    newObj.GetComponentInChildren<Text>().text = item.Name;
                    newObj.GetComponentInChildren<Button>().onClick.AddListener(delegate { SetSelectedIngredient(trans.gameObject, newObj); });
                }
            }
        }
    }

    private void PopulateSelectedButtons()
    {
        for (int i = 0; i < numIngredients; i++)
        {
            GameObject newObj = GameObject.Instantiate(selectedIngredientButton);
            newObj.transform.parent = selectedPanel.transform;
            currentButtons.Add(newObj);
            currentIngredients.Add(null);
        }
    }

    private void ClearCraftingMenu()
    {
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in selectedPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void SetSelectedIngredient(GameObject itemObj, GameObject button)
    {
        IItem item = itemObj.GetComponent<IItem>();
        for (int i = 0; i < currentButtons.Count; i++)
        {
            if (currentIngredients[i] == null)
            {
                currentIngredients[i] = itemObj;
                currentButtons[i].GetComponentInChildren<Text>().text = item.Name;
                currentButtons[i].GetComponentInChildren<Button>().onClick.AddListener(delegate { RemoveIngredient(i, button); });
                button.GetComponentInChildren<Button>().enabled = false;
                return;
            }
        }
    }

    private void RemoveIngredient(int ingredientIndex, GameObject buttonToReenable)
    {
        currentIngredients[ingredientIndex] = null;
        currentButtons[ingredientIndex].GetComponentInChildren<Text>().text = "";
        currentButtons[ingredientIndex].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        buttonToReenable.GetComponentInChildren<Button>().enabled = true;
    }

    public void DoCraft()
    {
        List<int> filteredList = new List<int>();
        foreach (GameObject obj in currentIngredients)
        {
            int id = obj.GetComponent<ICraftingIngredient>().CraftingID;
            if(id != -1)
            {
                filteredList.Add(id);
            } 
        }
        filteredList.Sort();

        CraftingRecipe newRecipe = recipeDictionary.SearchForRecipeMatch(filteredList);

        if(newRecipe != null)
        {
            foreach(GameObject obj in currentIngredients)
            {
                obj.transform.parent = null;
                Destroy(obj);
            }

            foreach (string prefabName in newRecipe.craftingResults)
            {
                GameObject newObj = GameObject.Instantiate(Resources.Load(prefabName) as GameObject);
                player.Bag.GetComponent<IBag>().Add(newObj, player.gameObject);
            }

            PopulateCraftingMenu();
        }
    }
}
