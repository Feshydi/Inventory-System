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
    private InventoryData _inventoryData;

    [Header("Auto Settings")]
    [SerializeField]
    private PlayerControls _inputActions;

    [SerializeField]
    private List<InventoryHolder> _inventoryHolders;

    [Header("Customizable settings")]
    [SerializeField]
    UIManager _uIManager;

    [SerializeField]
    private Logger _logger;

    #endregion

    #region Properties

    public List<InventoryHolder> InventoryHolders => _inventoryHolders;

    #endregion

    #region Methods

    private void Awake()
    {
        GetInventoryHolders();

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

    private void Inventory_performed(InputAction.CallbackContext context)
    {
        _uIManager.ChangeUIStateAndOpen(UIState.InventoryOpen);
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
