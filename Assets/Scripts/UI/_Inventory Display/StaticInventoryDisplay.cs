using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json.Linq;
using TMPro;

public class StaticInventoryDisplay : MonoBehaviour
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private InventoryData _inventoryData;

    [Header("Auto Settings")]
    [SerializeField]
    private InventoryHolder _currentInventory;

    [SerializeField]
    private TextMeshProUGUI _currentInventoryText;

    [SerializeField]
    private Vector2 _currentAnchoredPosition;

    [SerializeField]
    private Dictionary<InventoryDisplay, InventoryHolder> _inventoryHoldersDisplay;

    [Header("Customizable settings")]
    [SerializeField]
    private PlayerInventoryController _playerInventory;

    [SerializeField]
    private InventoryDisplay _inventoryDisplayPrefab;

    [SerializeField]
    private Logger _logger;

    [SerializeField]
    private float _swipeRange;

    #endregion

    #region Methods

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

                _logger.Log($"{inventoryHolder} instantiated as {slot} and added to dictionary", this);
            }
            else
                _logger.Log($"No inventory assigned to {inventoryHolder}", this);
        }

        if (_inventoryHoldersDisplay.Count > 0)
        {
            _currentInventory = _inventoryHoldersDisplay.First().Value;
            _currentInventoryText.text = _currentInventory.GetType().Name;
            _currentAnchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    private void Update()
    {
        if (_playerInventory.InventoryHolders.Count <= 0 || !GameManager.Instance.IsInventoryOpened)
            return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DisplayPreviousInventory(_currentInventory);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            DisplayNextInventory(_currentInventory);
        }
    }

    private void DisplayNextInventory(InventoryHolder currentInventory)
    {
        _currentInventory = _playerInventory.GetNextInventoryHolder(currentInventory);
        if (!currentInventory.Equals(_currentInventory))
            SwipeInventoryByX(-_swipeRange);

        _logger.Log($"Selected {_currentInventory}", this);
    }

    private void DisplayPreviousInventory(InventoryHolder currentInventory)
    {
        _currentInventory = _playerInventory.GetPreviousInventoryHolder(currentInventory);
        if (!currentInventory.Equals(_currentInventory))
            SwipeInventoryByX(_swipeRange);

        _logger.Log($"Selected {_currentInventory}", this);
    }

    private void SwipeInventoryByX(float xValue)
    {
        var startPos = GetComponent<RectTransform>().anchoredPosition;
        _currentAnchoredPosition += new Vector2(xValue, 0);
        LeanTween
            .moveX(GetComponent<RectTransform>(), _currentAnchoredPosition.x, _inventoryData.SwipeTime)
            .setEase(_inventoryData.SwipeCurve);

        _currentInventoryText.text = _currentInventory.GetType().Name;
    }

    #endregion

}
