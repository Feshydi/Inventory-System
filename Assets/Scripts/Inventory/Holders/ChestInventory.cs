using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : InventoryHolder, IInteractable
{

    #region Fields

    [SerializeField]
    private DynamicInventoryDisplay _inventoryDisplay;

    #endregion

    #region Methods

    public void Interact()
    {
        _inventoryDisplay.gameObject.SetActive(!_inventoryDisplay.gameObject.activeInHierarchy);

        if (!_inventoryDisplay.gameObject.activeInHierarchy)
        {
            _inventoryDisplay.gameObject.SetActive(true);
            _inventoryDisplay.RefreshDynamicInventory(_inventorySystem);
            return;
        }

        _inventoryDisplay.gameObject.SetActive(false);
    }

    #endregion

}
