using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class PlayerInventoryController : MonoBehaviour
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private PlayerData _playerData;

    [SerializeField]
    private PlayerControls _inputActions;

    [Header("Auto Settings")]
    [SerializeField]
    private List<InventoryHolder> _inventoryHolders;

    #endregion

    #region Properties

    public List<InventoryHolder> InventoryHolders => _inventoryHolders;

    #endregion

    #region Methods

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerData.IsInventoryOpened = false;

        GetInventoryHolders();

        //   Debug.Log(_inventoryHoldersNames[typeof(PlayerWeaponInventoryHolder).Name]);
        _inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
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

    private void GetInventoryHolders()
    {
        _inventoryHolders = new List<InventoryHolder>();
        foreach (var component in GetComponents<InventoryHolder>())
        {
            _inventoryHolders.Add(component);
        }
    }

    public InventoryHolder GetPreviousInventoryHolder(InventoryHolder previousOfThis)
    {
        return
            _inventoryHolders
            .TakeWhile(x => x != previousOfThis)
            .DefaultIfEmpty(_inventoryHolders[0])
            .LastOrDefault();
    }

    public InventoryHolder GetNextInventoryHolder(InventoryHolder nextOfThis)
    {
        return
            _inventoryHolders
            .SkipWhile(x => x != nextOfThis).Skip(1)
            .DefaultIfEmpty(_inventoryHolders[_inventoryHolders.Count - 1])
            .FirstOrDefault();
    }

    #endregion

}
