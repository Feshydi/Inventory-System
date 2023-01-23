using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicInventoryDisplay : InventoryDisplay
{

    #region Fields

    [SerializeField]
    private CanvasGroup _parentCanvasGroup;

    #endregion

    #region Methods

    private void Update()
    {
        if (_inventorySystem != null && _parentCanvasGroup.alpha == 0)
            ClearSlots();
    }

    public void RefreshDynamicInventory(InventorySystem inventoryToDisplay)
    {
        ClearSlots();
        Init(inventoryToDisplay);
        AssignSlots();
    }

    protected override void ClearSlots()
    {
        base.ClearSlots();
        _inventorySystem = null;
    }

    #endregion

}
