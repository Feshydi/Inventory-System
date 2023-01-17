using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialInventoryHolder : InventoryHolder
{

    #region Fields

    [Header("Static Data")]
    [SerializeField]
    private InventoryData _inventoryData;

    #endregion

    #region Methods

    private void Awake()
    {
        _inventorySize = _inventoryData.MaterialInventorySize;
        SetupNewInventorySystem();
    }

    #endregion

}
