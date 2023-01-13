using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventoryController : MonoBehaviour
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private PlayerData _playerData;

    [Header("Autosettings")]
    [SerializeField]
    private PlayerControls _inputActions;

    [Header("Customizable settings")]
    [SerializeField]
    private InventoryHolder[] _inventoryHolders;

    [SerializeField]
    private Dictionary<string, InventoryHolder> _inventoryHoldersNames;

    #endregion

    #region Properties

    public Dictionary<string, InventoryHolder> InventoryHoldersNames
    {
        get => _inventoryHoldersNames;
        set => _inventoryHoldersNames = value;
    }

    #endregion

    #region Methods

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        SetHoldersNames();

        //   Debug.Log(_inventoryHoldersNames[typeof(PlayerWeaponInventoryHolder).Name]);

        _inputActions = new PlayerControls();
        _inputActions.Player.Enable();
        _inputActions.Player.Inventory.performed += Inventory_performed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Inventory.performed -= Inventory_performed;
        _inputActions.Player.Disable();
    }

    // open/close static and close only dynamic inventories
    private void Inventory_performed(InputAction.CallbackContext context)
    {
        // set cursor
        Cursor.lockState = _playerData.IsInventoryOpened ? CursorLockMode.Locked : CursorLockMode.None;
        // need to fix screen flick after locked

        _playerData.IsInventoryOpened = !_playerData.IsInventoryOpened;

        // close static UI
        InventoryController.Instance.SetStaticInventoryActive(_playerData.IsInventoryOpened);

        // if close static UI, then close other
        if (!_playerData.IsInventoryOpened)
        {
            InventoryController.Instance.SetDynamicInventoryActive(false);
            InventoryController.Instance.SetCraftingActive(false);
        }
    }

    private void SetHoldersNames()
    {
        _inventoryHoldersNames = new Dictionary<string, InventoryHolder>();
        foreach (var item in _inventoryHolders)
        {
            _inventoryHoldersNames.Add(item.GetType().Name, item);
        }
    }

    #endregion

}
