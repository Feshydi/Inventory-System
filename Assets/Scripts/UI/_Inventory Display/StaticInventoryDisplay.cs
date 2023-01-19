using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json.Linq;
using TMPro;

[RequireComponent(typeof(SwipeEffect))]
public class StaticInventoryDisplay : MonoBehaviour
{

    #region Fields

    [Header("Auto Settings")]
    [SerializeField]
    private InventoryHolder _currentInventory;

    [SerializeField]
    private TextMeshProUGUI _currentInventoryText;

    [SerializeField]
    private SwipeEffect _swipeEffect;

    [SerializeField]
    private Dictionary<InventoryDisplay, InventoryHolder> _inventoryHoldersDisplay;

    [Header("Customizable settings")]
    [SerializeField]
    private UIManager _uIManager;

    [SerializeField]
    private PlayerInventoryController _playerInventory;

    [SerializeField]
    private InventoryDisplay _inventoryDisplayPrefab;

    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    private void Start()
    {
        if (_playerInventory.InventoryHolders.Count <= 0)
        {
            _logger.Log("No holders assigned to read", this);
            return;
        }

        _swipeEffect = GetComponent<SwipeEffect>();

        _inventoryHoldersDisplay = new Dictionary<InventoryDisplay, InventoryHolder>();

        foreach (var inventoryHolder in _playerInventory.InventoryHolders)
        {
            var inventorySystem = inventoryHolder.InventorySystem;

            if (inventorySystem != null)
            {
                var inventory = Instantiate(_inventoryDisplayPrefab, transform);
                inventory.Init(inventorySystem);
                inventory.AssignSlots();
                _inventoryHoldersDisplay.Add(inventory, inventoryHolder);

                _logger.Log($"{inventoryHolder} instantiated as {inventory} and added to dictionary", this);
            }
            else
                _logger.Log($"No inventory assigned to {inventoryHolder}", this);
        }

        if (_inventoryHoldersDisplay.Count > 0)
        {
            _currentInventory = _inventoryHoldersDisplay.First().Value;
            _currentInventoryText.text = _currentInventory.GetType().Name;
        }
    }

    private void Update()
    {
        if (_playerInventory.InventoryHolders.Count <= 0 || _uIManager.CurrentUIState.Equals(UIState.Close))
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DisplayInventory(true, _currentInventory);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DisplayInventory(false, _currentInventory);
        }
    }

    private void DisplayInventory(bool right, InventoryHolder currentInventory)
    {
        if (right)
            _currentInventory = _playerInventory.GetPreviousInventoryHolder(currentInventory);
        else
            _currentInventory = _playerInventory.GetNextInventoryHolder(currentInventory);

        if (!currentInventory.Equals(_currentInventory))
            _swipeEffect.Swipe(right);

        _logger.Log($"Selected {_currentInventory}", this);
        _currentInventoryText.text = _currentInventory.GetType().Name;
    }

    #endregion

}
