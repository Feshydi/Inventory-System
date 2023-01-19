using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHolder : InventoryHolder, IInteractable
{

    #region Fields

    [Header("Customizable settings")]
    [SerializeField]
    private DynamicInventoryDisplay _inventoryDisplay;

    [SerializeField]
    private UIManager _uIManager;

    #endregion

    #region Methods

    private void Awake()
    {
        SetupNewInventorySystem();
    }

    public void Interact(PlayerInventoryController player)
    {
        _uIManager.ChangeUIStateAndOpen(UIState.ContainerOpen);

        if (_uIManager.CurrentUIState.Equals(UIState.ContainerOpen))
            _inventoryDisplay.RefreshDynamicInventory(_inventorySystem);
    }

    public override string ToString()
    {
        return gameObject.name;
    }
    #endregion

}
