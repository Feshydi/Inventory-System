using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInventory : InventoryHolder, IInteractable
{

    #region Fields

    #endregion

    #region Properties

    #endregion

    #region Methods

    public void Interact()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested.Invoke(_inventorySystem);
    }

    #endregion

}
