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
    private PlayerInventoryController _playerInventory;

    [SerializeField]
    private InventoryDisplay _inventoryDisplayPrefab;

    [SerializeField]
    private Dictionary<InventoryDisplay, InventoryHolder> _inventoryHoldersDisplay;

    [SerializeField]
    private InventoryHolder _currentInventory;

    [SerializeField]
    private TextMeshProUGUI _currentInventoryText;

    [SerializeField]
    private Vector2 _currentAnchoredPosition;

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
                // slot.gameObject.SetActive(false);

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
        {
            StartCoroutine(SmoothMove(new Vector2(-590, 0), 0.3f));
            _currentInventoryText.text = _currentInventory.GetType().Name;
        }

        _logger.Log($"Selected {_currentInventory}", this);
    }

    private void DisplayPreviousInventory(InventoryHolder currentInventory)
    {
        _currentInventory = _playerInventory.GetPreviousInventoryHolder(currentInventory);
        if (!currentInventory.Equals(_currentInventory))
        {
            StartCoroutine(SmoothMove(new Vector2(590, 0), 0.3f));
            _currentInventoryText.text = _currentInventory.GetType().Name;
        }

        _logger.Log($"Selected {_currentInventory}", this);
    }

    IEnumerator SmoothMove(Vector2 additionalPos, float seconds)
    {
        var startPos = GetComponent<RectTransform>().anchoredPosition;
        _currentAnchoredPosition += additionalPos;
        float t = 0f;
        while (t <= 1)
        {
            t += Time.deltaTime / seconds;
            GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPos, _currentAnchoredPosition, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }
    }

    private void SetActiveCurrentInventory(bool value)
    {
        _inventoryHoldersDisplay.FirstOrDefault(x => x.Value == _currentInventory).Key.gameObject.SetActive(value);
    }

    #endregion

}
