using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeDisplay : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private GameObject _recipePrefab;

    [SerializeField]
    private BaseSlotManager _slotPrefab;

    [SerializeField]
    private List<CraftingRecipe> _craftingRecipes;

    #endregion

    #region Properties

    public GameObject RecipePrefab => _recipePrefab;

    public BaseSlotManager SlotPrefab => _slotPrefab;

    #endregion

    #region Methods

    private void Awake()
    {
        DisplayCraftingRecipes();
    }

    private void DisplayCraftingRecipes()
    {
        // create every recipe
        foreach (var craftingRecipe in _craftingRecipes)
        {
            // get new recipe transform
            var recipe = Instantiate(_recipePrefab, transform).transform;

            // instantiate every material
            foreach (var material in craftingRecipe.Materials)
            {
                var slot = Instantiate(_slotPrefab, recipe);
                slot.Init(new InventorySlot(material.ItemObject, material.Amount));
            }

            // instantiate empty slot
            Instantiate(_slotPrefab, recipe);

            // instantiate every result
            foreach (var result in craftingRecipe.Results)
            {
                var slot = Instantiate(_slotPrefab, recipe);
                slot.Init(new InventorySlot(result.ItemObject, result.Amount));
            }
        }
    }

    private void Clear()
    {
        foreach (Transform _transform in transform)
        {
            Destroy(_transform.gameObject);
        }
    }

    #endregion

}
