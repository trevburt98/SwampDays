using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeDictionary
{
    List<CraftingRecipe> recipeList;
    public RecipeDictionary()
    {
        recipeList = new List<CraftingRecipe>();

        recipeList.Add(new CraftingRecipe("Mama's helmet", new List<int>{ 0, 1 }, new List<string>{ "Equipment/hEquipEx" }));
    }

    public CraftingRecipe SearchForRecipeMatch(List<int> ingredientIds)
    {
        CraftingRecipe recipeToReturn = null;

        foreach(CraftingRecipe recipe in recipeList)
        {
            if(recipe.ingredientIdList.Count == ingredientIds.Count)
            {
                bool match = true;

                for(int i = 0; i < recipe.ingredientIdList.Count; i++)
                {
                    if(recipe.ingredientIdList[i] != ingredientIds[i])
                    {
                        match = false;
                    }
                }

                if(match)
                {
                    recipeToReturn = recipe;
                    ToggleRecipeKnown(recipe);
                    break;
                }
            }
        }

        return recipeToReturn;
    }

    public void ToggleRecipeKnown(CraftingRecipe recipe)
    {
        recipe.known = true;
    }
}
