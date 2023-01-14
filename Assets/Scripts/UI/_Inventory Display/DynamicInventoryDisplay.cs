using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicInventoryDisplay : InventoryDisplay
{

    #region Fields

    #endregion

    #region Methods

    public void RefreshDynamicInventory(InventorySystem inventoryToDisplay)
    {
        Init(_inventorySystem);

        ClearSlots();
        AssignSlots();
    }

    #endregion

}
