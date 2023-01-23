using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInventoryHolder : InventoryHolder
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private InventoryData _inventoryData;

    #endregion

    #region Methods

    private void Awake()
    {
        _inventorySize = _inventoryData.FoodInventorySize;
        SetupNewInventorySystem();
    }

    #endregion

}
