using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CraftingRecipeDisplay : MonoBehaviour
{

    #region Fields

    [Header("Auto Settings")]
    [SerializeField]
    private PlayerInventoryController _player;

    [SerializeField]
    private CraftSystem _craftSystem;

    [SerializeField]
    private Dictionary<Button, CraftingRecipe> _buttonForCraft;

    [Header("Customizable settings")]
    [SerializeField]
    private CanvasGroup _parentCanvasGroup;

    [SerializeField]
    private GameObject _recipePrefab;

    [SerializeField]
    private BaseSlotManager _slotPrefab;

    [SerializeField]
    private Button _buttonPrefab;

    [SerializeField]
    private InventoryDescription _description;

    [SerializeField]
    private Logger _logger;

    #endregion

    #region Properties

    public GameObject RecipePrefab => _recipePrefab;

    public BaseSlotManager SlotPrefab => _slotPrefab;

    #endregion

    #region Methods

    private void Awake()
    {
        _craftSystem = new CraftSystem();
        _buttonForCraft = new Dictionary<Button, CraftingRecipe>();
    }

    private void Update()
    {
        if (_player != null && _parentCanvasGroup.alpha == 0)
            Clear();
    }

    public void Init(PlayerInventoryController player, CraftKeeper craftKeeper)
    {
        Clear();

        _player = player;
        foreach (var recipe in craftKeeper.CraftSystem.CraftingRecipes)
        {
            _craftSystem.CraftingRecipes.Add(recipe);
        }

        DisplayCraftingRecipes();
    }

    public bool DisplayCraftingRecipes()
    {
        // create every recipe
        foreach (var craftingRecipe in _craftSystem.CraftingRecipes)
        {
            // get new recipe transform
            var recipe = Instantiate(_recipePrefab, transform).transform;

            // instantiate every material
            foreach (var material in craftingRecipe.Materials)
            {
                var slot = Instantiate(_slotPrefab, recipe);
                slot.Init(new InventorySlot(material.ItemObject, material.Amount));
            }

            // instantiate button for craft
            var button = Instantiate(_buttonPrefab, recipe);
            _buttonForCraft.Add(button, craftingRecipe);
            button.onClick.AddListener(delegate { CraftItem(EventSystem.current.currentSelectedGameObject.GetComponent<Button>()); });

            // instantiate every result
            foreach (var result in craftingRecipe.Results)
            {
                var slot = Instantiate(_slotPrefab, recipe);
                slot.Init(new InventorySlot(result.ItemObject, result.Amount));
            }
        }
        return true;
    }

    public void CraftItem(Button button)
    {
        if (_buttonForCraft[button].CanCraft(_player))
        {
            _buttonForCraft[button].Craft(_player);
            StartCoroutine(ShowWarning("Сompleted"));
        }
        else
        {
            StartCoroutine(ShowWarning("Not enough items or space"));
        }
    }

    IEnumerator ShowWarning(string message)
    {
        _description.ShowDescription(message, true);
        yield return new WaitForSeconds(1);
        _description.ShowDescription("", false);
    }

    private void Clear()
    {
        _player = null;
        _craftSystem = new CraftSystem();

        _buttonForCraft = new Dictionary<Button, CraftingRecipe>();

        foreach (Transform _transform in transform)
        {
            Destroy(_transform.gameObject);
        }
    }

    #endregion

}
