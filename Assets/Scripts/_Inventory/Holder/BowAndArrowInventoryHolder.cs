using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAndArrowInventoryHolder : InventoryHolder
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private InventoryData _inventoryData;

    #endregion

    #region Methods

    private void Awake()
    {
        _inventorySize = _inventoryData.BowAndArrowInventorySize;
        SetupNewInventorySystem();
    }

    #endregion

}
