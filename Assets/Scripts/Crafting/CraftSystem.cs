using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CraftSystem
{

    #region Fields

    [SerializeField]
    private List<CraftingRecipe> _craftingRecipes = new List<CraftingRecipe>();

    #endregion

    #region Properties

    public List<CraftingRecipe> CraftingRecipes => _craftingRecipes;

    public int CraftinRecipesSize => _craftingRecipes.Count;

    #endregion

    #region Methods

    #endregion

}
