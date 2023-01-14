using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json.Linq;

public class StaticInventoryDisplay : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private PlayerInventoryController _playerInventory;

    [SerializeField]
    private InventoryDisplay _inventoryDisplayPrefab;

    [SerializeField]
    private Dictionary<InventoryDisplay, InventoryHolder> _inventoryHoldersDisplay;

    [SerializeField]
    private InventoryHolder _currentInventory;

    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    private void Awake()
    {
        //gameObject.SetActive(false);
    }

    private void Start()
    {
        if (_playerInventory.InventoryHolders.Count <= 0)
        {
            _logger.Log("No holders assigned to read", this);
            return;
        }

        _inventoryHoldersDisplay = new Dictionary<InventoryDisplay, InventoryHolder>();

        foreach (var inventoryHolder in _playerInventory.InventoryHolders)
        {
            var inventorySystem = inventoryHolder.InventorySystem;

            if (inventorySystem != null)
            {
                var slot = Instantiate(_inventoryDisplayPrefab, transform);
                slot.Init(inventorySystem);
                slot.AssignSlots();
                _inventoryHoldersDisplay.Add(slot, inventoryHolder);
                slot.gameObject.SetActive(false);

                _logger.Log($"{inventoryHolder} instantiated as {slot} and added to dictionary", this);
            }
            else
                _logger.Log($"No inventory assigned to {inventoryHolder}", this);
        }

        if (_inventoryHoldersDisplay.Count > 0)
        {
            _currentInventory = _inventoryHoldersDisplay.First().Value;
            SetActiveCurrentInventory(true);
        }
    }

    private void Update()
    {
        if (_playerInventory.InventoryHolders.Count <= 0)
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DisplayPreviousInventory();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DisplayNextInventory();
        }
    }

    private void DisplayNextInventory()
    {
        SetActiveCurrentInventory(false);
        _currentInventory = _playerInventory.GetNextInventoryHolder(_currentInventory);
        SetActiveCurrentInventory(true);

        _logger.Log($"Selected {_currentInventory}", this);
    }

    private void DisplayPreviousInventory()
    {
        SetActiveCurrentInventory(false);
        _currentInventory = _playerInventory.GetPreviousInventoryHolder(_currentInventory);
        SetActiveCurrentInventory(true);

        _logger.Log($"Selected {_currentInventory}", this);
    }

    private void SetActiveCurrentInventory(bool value)
    {
        _inventoryHoldersDisplay.FirstOrDefault(x => x.Value == _currentInventory).Key.gameObject.SetActive(value);
    }

    #endregion

}
