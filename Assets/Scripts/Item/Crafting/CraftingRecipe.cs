using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipe
{
    public string recipeName;

    public List<int> ingredientIdList;
    public List<string> craftingResults;

    public bool known = false;

    public CraftingRecipe(string name, List<int> ingredients, List<string> result)
    {
        recipeName = name;

        ingredientIdList = new List<int>();
        craftingResults = new List<string>();

        foreach(int i in ingredients)
        {
            ingredientIdList.Add(i);
        }

        foreach(string prefabName in result)
        {
            craftingResults.Add(prefabName);
        }
    }
}
