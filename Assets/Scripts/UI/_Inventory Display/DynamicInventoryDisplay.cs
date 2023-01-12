using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicInventoryDisplay : InventoryDisplay
{

    #region Fields

    #endregion

    #region Methods

    private void OnDisable()
    {
        if (_inventorySystem != null)
            _inventorySystem.OnInventorySlotChanged -= UpdateSlot;
    }

    public void RefreshDynamicInventory(InventorySystem inventoryToDisplay)
    {
        _inventorySystem = inventoryToDisplay;

        if (_inventorySystem != null)
            _inventorySystem.OnInventorySlotChanged += UpdateSlot;

        ClearSlots();
        AssignSlots(_inventorySystem);
    }

    #endregion

}
